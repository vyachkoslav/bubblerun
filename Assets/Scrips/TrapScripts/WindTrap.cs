using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrap : Trap
{
    [SerializeField] private Vector2 _windDirection;
    [SerializeField] private float _windStrength;
    [SerializeField] private float _windDuration;
    [SerializeField] Material _lineMaterial;

    private Collider2D coll;
    private LineRenderer lr;

    private Coroutine coroutine;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        lr = gameObject.AddComponent<LineRenderer>();
        Bounds bounds = coll.bounds;
        Vector3[] corners =
        {
            bounds.max,
            new (bounds.max.x, bounds.min.y, 0),
            bounds.min,
            new (bounds.min.x, bounds.max.y, 0)
        };
        lr.positionCount = 4;
        lr.SetPositions(corners);
        lr.loop = true;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.material = _lineMaterial;
    }

    protected override bool IsRunning() => coroutine != null;
    protected override void ActivateTrap()
    {
        coroutine = StartCoroutine(ApplyWind());
    }

    IEnumerator ApplyWind()
    {
        lr.startColor = Color.red;
        lr.endColor = Color.red;

        WaitForEndOfFrame wait = new();
        List<Collider2D> overlaps = new();
        Rigidbody2D rb;

        float t = 0;
        while (t < _windDuration)
        {
            coll.Overlap(overlaps);
            foreach (var item in overlaps)
            {
                if (!item.CompareTag("Player")) continue;
                rb = item.GetComponent<Rigidbody2D>();
                rb.AddForce(_windDirection.normalized * _windStrength * 100 * Time.deltaTime);
            }

            t += Time.deltaTime;
            overlaps.Clear();
            yield return wait;
        }

        lr.startColor = Color.green;
        lr.endColor = Color.green;

        coroutine = null;
    }
}
