using System;
using UnityEngine;

public class AttachToScreen : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private Transform attachedToBottom;
    [SerializeField] private Transform attachedToTop;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (attachedToBottom)
        {
            Vector3 bottom = camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, camera.nearClipPlane));
            attachedToBottom.position = bottom;
        }

        if (attachedToTop)
        {
            Vector3 top = camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, camera.nearClipPlane));
            attachedToTop.position = top;
        }
    }
}