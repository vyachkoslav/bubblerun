using UnityEngine;

public class DestroyBubbleOnHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        collision.GetComponent<BubbleController>().DestroyBubble();
    }
}
