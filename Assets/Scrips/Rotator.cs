using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;
    private float curAngle = 0;
    void Update()
    {
        curAngle += speed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(curAngle, Vector3.forward);
    }
}
