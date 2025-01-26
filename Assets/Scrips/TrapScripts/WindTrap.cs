using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrap : Trap
{
    [SerializeField] private Vector2 _windDirection;
    [SerializeField] private float _windStrength;
    [SerializeField] private float _windDuration;
    List<Collider2D> overlaps = new();
    private Collider2D coll;
    
    private Coroutine coroutine;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public override bool IsRunning() => coroutine != null;
    protected override void ActivateTrap()
    {
        coroutine = StartCoroutine(ApplyWind());
    }

    IEnumerator ApplyWind()
    {
        overlaps.Clear();
        float t = 0;
        while (t < _windDuration)
        {
            coll.Overlap(overlaps);
            foreach (var item in overlaps)
            {
                if (!item.CompareTag("Player")) continue;
                item.attachedRigidbody.AddForce(_windDirection.normalized * (_windStrength * 100 * Time.deltaTime));
            }

            t += Time.deltaTime;
            overlaps.Clear();
            yield return null;
        }
        coroutine = null;
        Finished();
    }
}
