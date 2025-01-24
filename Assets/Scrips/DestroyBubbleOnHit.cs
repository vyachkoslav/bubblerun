using UnityEngine;

public class DestroyBubbleOnHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        collision.GetComponent<BubbleController>().DestroyBubble();
    }
}
