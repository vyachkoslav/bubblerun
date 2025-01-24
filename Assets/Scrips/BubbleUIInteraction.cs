using System;
using UnityEngine;
using UnityEngine.Events;

public class BubbleUIInteraction : MonoBehaviour
{
    [SerializeField] private Transform bubbleSpawn;
    [SerializeField] private AudioSource bubbleSoundPlayer;
    [SerializeField] private AudioSource bubbleDestroySoundPlayer;
    
    private BubbleUI hoveredBubbleUI;
    private bool interactionEnabled = true;
    
    public void SetEnabled(bool enabled)
    {
        interactionEnabled = enabled;
    }

    private void OnMouseEnter()
    {
        if (!interactionEnabled) return;
        bubbleSoundPlayer.Play();
        if(!hoveredBubbleUI)
            transform.position = bubbleSpawn.position;
        else
            hoveredBubbleUI.OnInteracted.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactionEnabled) return;
        if (other.TryGetComponent(out BubbleUI ui))
        {
            hoveredBubbleUI = ui;
            ui.OnHover.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!interactionEnabled) return;
        if (hoveredBubbleUI && other.gameObject == hoveredBubbleUI.gameObject)
        {
            hoveredBubbleUI.OnHoverEnd.Invoke();
            hoveredBubbleUI = null;
        }
    }

    private void OnDestroy()
    {
        bubbleDestroySoundPlayer.Play();
    }
}
