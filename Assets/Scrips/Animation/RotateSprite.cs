using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RotateSprite : MonoBehaviour
{
    [SerializeField] Sprite _upSprite;
    [SerializeField] Sprite _upLeftSprite;

    private new SpriteRenderer renderer;

    public void Init(Sprite upSprite, Sprite upLeftSprite)
    {
        _upSprite = upSprite;
        _upLeftSprite = upLeftSprite;
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateTransformAndSprite(Vector2 dir)
    {
        if (dir.y > 0.5f)
        {
            if (dir.x < -0.5f)
            {
                renderer.sprite = _upLeftSprite;
                transform.rotation = Quaternion.identity;
            }
            else if (dir.x > 0.5f)
            {
                renderer.sprite = _upLeftSprite;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                renderer.sprite = _upSprite;
                transform.rotation = Quaternion.identity;
            }
        }
        else if (dir.y < -0.5f)
        {
            if (dir.x < -0.5f)
            {
                renderer.sprite = _upLeftSprite;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.x > 0.5f)
            {
                renderer.sprite = _upLeftSprite;
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
            else
            {
                renderer.sprite = _upSprite;
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
        }
        else
        {
            if (dir.x < -0.5f)
            {
                renderer.sprite = _upSprite;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.x > 0.5f)
            {
                renderer.sprite = _upSprite;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
}
