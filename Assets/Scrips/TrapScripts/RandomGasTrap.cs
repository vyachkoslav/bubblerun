using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGasTrap : Trap
{
    
    [SerializeField] GameObject smokePrefab;
    [SerializeField] GameObject warnPrefab;
    [SerializeField] private RectTransform range;
    [SerializeField] private float warningDuration = 0.5f;
    [SerializeField] private float durationToGetMaxRadius = 3;
    [SerializeField] private float durationAtMaxRadius = 1;
    [SerializeField] private float radius = 3;
    
    private GameObject warnPoint;
    private GameObject smoke;
    private Coroutine trapRoutine;

    private void Awake()
    {
        warnPoint = Instantiate(warnPrefab, transform);
        smoke = Instantiate(smokePrefab, transform);
        warnPoint.SetActive(false);
        smoke.SetActive(false);
    }

    protected override void ActivateTrap()
    {
        trapRoutine = StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        Vector3 trapPoint = range.position;
        trapPoint.x += Random.Range(range.rect.xMin, range.rect.xMax);
        trapPoint.y += Random.Range(range.rect.yMin, range.rect.yMax);
        warnPoint.transform.position = trapPoint;
        smoke.transform.position = trapPoint;
        smoke.transform.localScale = Vector3.zero;
        warnPoint.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        warnPoint.SetActive(false);
        smoke.SetActive(true);
        float elapsed = 0;
        while (elapsed < durationToGetMaxRadius)
        {
            smoke.transform.localScale = Vector3.Lerp(Vector3.zero,
                Vector3.one * radius,
                elapsed / durationToGetMaxRadius);
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(durationAtMaxRadius);
        smoke.SetActive(false);
        
        trapRoutine = null;
        Finished();
    }

    protected override bool IsRunning() => trapRoutine != null;
}
