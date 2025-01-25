using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomVerticalProjectileTrap : Trap
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private RectTransform range;
    [SerializeField] private float projectileWidth = 0.5f;
    [SerializeField] private int projectileCount = 10;
    [SerializeField] private float timeToMoveProjectiles = 3;
    private List<LineRenderer> warnLineRenderers = new();
    private List<GameObject> projectiles = new();
    private List<int> lines;
    private float slotWidth;
    private Vector3 startPos;
    private Vector3 endPos;

    private Coroutine currentTrapRoutine;

    private void Awake()
    {
        lines = Enumerable.Range(0, (int)(range.rect.width / projectileWidth)).ToList();
        slotWidth = range.rect.width / lines.Count;

        Vector3[] corners = new Vector3[4];
        range.GetWorldCorners(corners);
        startPos = corners[1];
        endPos = corners[0];
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
            warnLineRenderers[i].startWidth = slotWidth;
            warnLineRenderers[i].endWidth = slotWidth;
            warnLineRenderers[i].startColor = Color.red;
            warnLineRenderers[i].endColor = Color.red;
            warnLineRenderers[i].enabled = false;
            warnLineRenderers[i].material = lineMaterial;
        }
    }

    protected override bool IsRunning() => currentTrapRoutine != null;
    protected override void ActivateTrap()
    {
        currentTrapRoutine = StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            var rn = Random.Range(i, lines.Count);
            (lines[i], lines[rn]) = (lines[rn], lines[i]);
            (Vector3 firstPos, Vector3 secondPos) = (startPos, endPos);
            var deltaX = projectileWidth * lines[i] + projectileWidth / 2;
            firstPos.x += deltaX;
            secondPos.x = firstPos.x;
            warnLineRenderers[i].SetPositions(new Vector3[] { firstPos, secondPos });
            warnLineRenderers[i].enabled = true;
            projectiles[i].transform.position = firstPos;
        }
        yield return new WaitForSeconds(0.5f);
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
                (Vector3 firstPos, Vector3 secondPos) = (startPos, endPos);
                var deltaX = projectileWidth * lines[i] + projectileWidth / 2;
                firstPos.x += deltaX;
                secondPos.x = firstPos.x;
                projectiles[i].transform.position = Vector3.Lerp(firstPos, secondPos, timeWent / timeToMoveProjectiles);
            }
            timeWent += Time.deltaTime;
            yield return null;
        }

        projectiles.ForEach(x => x.SetActive(false));
        currentTrapRoutine = null;
    }
}