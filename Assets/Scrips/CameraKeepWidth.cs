using System;
using UnityEngine;

public class CameraKeepWidth : MonoBehaviour
{
    [SerializeField] private Vector2 referenceSize;
    private new Camera camera;
    private float screenRatio;
    private float initSize;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        initSize = camera.orthographicSize;
        screenRatio = camera.orthographicSize/(referenceSize.x / referenceSize.y);
    }

    void Update()
    {
        screenRatio = initSize/(referenceSize.x / referenceSize.y);
        camera.orthographicSize = screenRatio*((float)camera.pixelHeight/camera.pixelWidth);
    }
}
