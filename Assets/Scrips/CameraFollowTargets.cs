using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTargets : MonoBehaviour
{
    public bool useFollow = true;

    [SerializeField] List<Transform> _targets = new();
    [SerializeField] float paddingToTop;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!useFollow) return;

        int count = 0;
        float targetY = 0;
        float maxY = float.MinValue;
        foreach (var target in _targets)
        {
            if (!target || !target.gameObject.activeSelf) continue;
            count++;
            targetY += target.position.y;
            if (target.position.y > maxY)
                maxY = target.position.y;
        }

        targetY /= count;

        var pos = transform.position;
        float paddedY = maxY + paddingToTop - cam.orthographicSize;
        pos.y = targetY > paddedY ? targetY : paddedY;
        transform.position = pos;
    }
}