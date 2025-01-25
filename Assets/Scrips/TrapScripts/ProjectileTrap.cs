using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] float _projectileSpeed;
    [SerializeField] Material _lineMaterial;

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
    }

    protected override bool IsRunning() => projectile != null;
    protected override void ActivateTrap()
    {
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        projectile = Instantiate(_projectilePrefab, _startPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile == null) return;

        if (Vector3.Distance(projectile.transform.position, _endPos.position) < 0.05f)
        {
            Destroy(projectile);
            lr.startColor = Color.green;
            lr.endColor = Color.green;
            return;
        }

        Vector3 newPos = projectile.transform.position;
        newPos += (_endPos.position - _startPos.position) * Time.deltaTime * (_projectileSpeed/100);

        projectile.transform.position = newPos;
    }
}
