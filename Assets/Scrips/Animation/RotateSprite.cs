using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Animator))]
public class RotateSprite : MonoBehaviour
{
    private Animator anim;

    //public void Init(Sprite upSprite, Sprite upLeftSprite)
    //{
    //    _upSprite = upSprite;
    //    _upLeftSprite = upLeftSprite;
    //}

    private void Awake()
    {
        //renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void UpdateTransformAndSprite(Vector2 dir)
    {
        anim.SetFloat("Velocity", dir.magnitude);

        bool diag = !(dir.y == 0 || dir.x == 0);

        anim.SetBool("Diag", diag);

        if (dir.y > 0.5f)
        {
            if (dir.x < -0.5f)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (dir.x > 0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
        else if (dir.y < -0.5f)
        {
            if (dir.x < -0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.x > 0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -180);
            }
        }
        else
        {
            if (dir.x < -0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.x > 0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
}
