using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTargets : MonoBehaviour
{
    [SerializeField] List<Transform> _targets = new();

    private void Update()
    {
        float targetY = new();

        _targets.ForEach(t =>  targetY += t.position.y);

         targetY *= 1f / _targets.Count;

         var pos = transform.position;
         pos.y = targetY;
         transform.position = pos;

        _targets.ForEach(t => Debug.DrawLine(t.position, transform.position, Color.red));
    }
}
