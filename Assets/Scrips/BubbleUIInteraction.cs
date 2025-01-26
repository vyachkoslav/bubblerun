using System;
using UnityEngine;
using UnityEngine.Events;

public class BubbleUIInteraction : MonoBehaviour
{
    [SerializeField] private Transform bubbleSpawn;
    [SerializeField] private AudioSource bubbleSoundPlayer;
    [SerializeField] private AudioSource bubbleDestroySoundPlayer;
    private Rigidbody2D bubbleRigidBody;
    
    private BubbleUI hoveredBubbleUI;
    private bool interactionEnabled = true;

    private void Awake()
    {
        bubbleRigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetEnabled(bool enabled)
    {
        interactionEnabled = enabled;
    }

    private void OnMouseEnter()
    {
        if (!interactionEnabled) return;
        Pop();
    }

    public void Pop()
    {
        bubbleSoundPlayer.Play();
        bubbleRigidBody.angularVelocity = 0;
        bubbleRigidBody.linearVelocity = Vector2.zero;
        
        if(!hoveredBubbleUI)
            transform.position = bubbleSpawn.position;
        else
            hoveredBubbleUI.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactionEnabled) return;
        if (other.TryGetComponent(out BubbleUI ui))
        {
            if(hoveredBubbleUI)
                hoveredBubbleUI.UnHover();
            hoveredBubbleUI = ui;
            ui.Hover();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!interactionEnabled) return;
        if (hoveredBubbleUI && other.gameObject == hoveredBubbleUI.gameObject)
        {
            hoveredBubbleUI.UnHover();
            hoveredBubbleUI = null;
        }
    }

    private void OnDestroy()
    {
        if(bubbleDestroySoundPlayer)
            bubbleDestroySoundPlayer.Play();
    }
}
