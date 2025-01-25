using System.Collections;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] float _timeToMove;
    [SerializeField] Material _lineMaterial;

    private Coroutine projectileRoutine;
    private GameObject projectile;
    private LineRenderer lr;

    private void Awake()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.SetPosition(0, _startPos.position);
        lr.SetPosition(1, _endPos.position);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.material = _lineMaterial;
        
        projectile = Instantiate(_projectilePrefab, _startPos.position, Quaternion.identity);
        projectile.SetActive(false);
    }

    public override bool IsRunning() => projectileRoutine != null;
    protected override void ActivateTrap()
    {
        projectileRoutine = StartCoroutine(SendProjectile());
    }

    private IEnumerator SendProjectile()
    {
        projectile.SetActive(true);
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        float elapsedTime = 0;
        while (elapsedTime < _timeToMove)
        {
            projectile.transform.position = Vector3.Lerp(_startPos.position, _endPos.position, elapsedTime / _timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        projectile.SetActive(false);
        projectileRoutine = null;
        Finished();
    }
}