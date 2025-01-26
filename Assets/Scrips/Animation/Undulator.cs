using UnityEngine;

public class Undulator : MonoBehaviour
{
    [SerializeField] float _amplitude = 1;
    [SerializeField] float _frequency = 1;
    [SerializeField] float _offset = 0;

    private float originalY;
    private float index = 0;

    private void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        index += Time.deltaTime;

        float y = _amplitude * Mathf.Sin((index + _offset) * _frequency);

        y = Mathf.Round(y * 16.0f) * (1.0f/16.0f);

        transform.position = new Vector3(transform.position.x, originalY + y, transform.position.z);

        index %= (2 * Mathf.PI);
    }
}
