using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTargets : MonoBehaviour
{
    [SerializeField] List<Transform> _targets = new();

    private void Update()
    {
        Vector3 camtarget = new();

        _targets.ForEach(t => camtarget += t.position);

        camtarget *= 1f / _targets.Count;

        camtarget.z = transform.position.z;

        transform.position = camtarget;

        _targets.ForEach(t => Debug.DrawLine(t.position, transform.position, Color.red));
    }
}
