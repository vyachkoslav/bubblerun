using System.Collections;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] float _timeToMove;
    [SerializeField] float _shotDelay;

    private Coroutine projectileRoutine;
    private GameObject projectile;

    private void Awake()
    {
        projectile = Instantiate(_projectilePrefab, _startPos.position, _projectilePrefab.transform.rotation);
        projectile.SetActive(false);
    }

    public override bool IsRunning() => projectileRoutine != null;
    protected override void ActivateTrap()
    {
        projectileRoutine = StartCoroutine(SendProjectile());
    }

    private IEnumerator SendProjectile()
    {
        yield return new WaitForSeconds(_shotDelay);
        projectile.SetActive(true);        
        float elapsedTime = 0;
        while (elapsedTime < _timeToMove)
        {
            projectile.transform.position = Vector3.Lerp(_startPos.position, _endPos.position, elapsedTime / _timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        projectile.SetActive(false);
        projectileRoutine = null;
        Finished();
    }
}