using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RadialProjectileTrap : Trap
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private RectTransform range;
    [SerializeField] private float projectileWidth = 0.5f;
    [SerializeField] private int projectileCount = 10;
    [SerializeField] private float timeToMoveProjectiles = 3;
    [SerializeField] private float warnDelay = 0.5f;
    [SerializeField] private float projectileDistance = 20f;
    private List<LineRenderer> warnLineRenderers = new();
    private List<GameObject> projectiles = new();
    private List<Vector3> projectileEndPositions = new();
    private float angleBetweenProjectiles;

    private Coroutine currentTrapRoutine;

    private void Awake()
    {
        projectileEndPositions = new(new Vector3[projectileCount]);
        angleBetweenProjectiles = 360.0f/projectileCount;
        Vector3[] corners = new Vector3[4];
        range.GetWorldCorners(corners);
        var projectilesParent = new GameObject("Projectiles"); 
        var linesParent = new GameObject("Lines");
        projectilesParent.transform.position -= new Vector3(0, 0, 0.5f);
        projectilesParent.transform.parent = transform;
        linesParent.transform.parent = transform;
        for (int i = 0; i < projectileCount; i++)
        {
            var go = new GameObject();
            warnLineRenderers.Add(go.AddComponent<LineRenderer>());
            projectiles.Add(Instantiate(projectilePrefab, projectilesParent.transform));
            projectiles[i].SetActive(false);
            warnLineRenderers[i].transform.parent = linesParent.transform;
            warnLineRenderers[i].positionCount = 2;
            warnLineRenderers[i].startWidth = projectileWidth;
            warnLineRenderers[i].endWidth = projectileWidth;
            warnLineRenderers[i].startColor = Color.red;
            warnLineRenderers[i].endColor = Color.red;
            warnLineRenderers[i].enabled = false;
            warnLineRenderers[i].material = lineMaterial;
        }
    }

    public override bool IsRunning() => currentTrapRoutine != null;
    protected override void ActivateTrap()
    {
        currentTrapRoutine = StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        Vector3 trapPoint = range.position;
        trapPoint.x += Random.Range(range.rect.xMin, range.rect.xMax);
        trapPoint.y += Random.Range(range.rect.yMin, range.rect.yMax);
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < projectileCount; i++)
        {
            angle += angleBetweenProjectiles;
            Vector3 endPoint = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * angle),
                Mathf.Cos(Mathf.Deg2Rad * angle),
                0) * projectileDistance;
            endPoint += trapPoint;
            projectileEndPositions[i] = endPoint;
            warnLineRenderers[i].SetPositions(new Vector3[] { trapPoint, endPoint });
            warnLineRenderers[i].enabled = true;
            projectiles[i].transform.position = trapPoint;
        }
        yield return new WaitForSeconds(warnDelay);
        for (int i = 0; i < projectileCount; i++)
        {
            projectiles[i].SetActive(true);
            warnLineRenderers[i].enabled = false;
        }
        float timeWent = 0;
        while (timeWent < timeToMoveProjectiles)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                projectiles[i].transform.position = Vector3.Lerp(
                    trapPoint,
                    projectileEndPositions[i],
                    timeWent / timeToMoveProjectiles);
            }
            timeWent += Time.deltaTime;
            yield return null;
        }

        projectiles.ForEach(x => x.SetActive(false));
        currentTrapRoutine = null;
        Finished();
    }
}