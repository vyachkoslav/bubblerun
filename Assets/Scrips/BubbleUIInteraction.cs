using System;
using UnityEngine;
using UnityEngine.Events;

public class BubbleUIInteraction : MonoBehaviour
{
    [SerializeField] private Transform bubbleSpawn;
    [SerializeField] private AudioSource bubbleSoundPlayer;
    private BubbleUI hoveredBubbleUI;

    private void OnMouseEnter()
    {
        bubbleSoundPlayer.Play();
        if(!hoveredBubbleUI)
            transform.position = bubbleSpawn.position;
        else
        {
            hoveredBubbleUI.OnInteracted.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BubbleUI ui))
        {
            hoveredBubbleUI = ui;
            ui.OnHover.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hoveredBubbleUI && other.gameObject == hoveredBubbleUI.gameObject)
        {
            hoveredBubbleUI.OnHoverEnd.Invoke();
            hoveredBubbleUI = null;
        }
    }
}
