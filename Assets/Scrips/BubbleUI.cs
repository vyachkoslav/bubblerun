using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleUI : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.white;
    [SerializeField] private List<SpriteRenderer> renderers = new();
    private int hovered = 0;

    public void Interact()
    {
        OnInteracted.Invoke();
    }
    
    public void Hover()
    {
        hovered += 1;
        if (hovered == 1)
        {
            renderers.ForEach(r => r.color = hoverColor);
            OnHover.Invoke();
        }
    }

    public void UnHover()
    {
        Debug.Assert(hovered != 0);
        hovered -= 1;
        if (hovered == 0)
        {
            renderers.ForEach(r => r.color = defaultColor);
            OnHoverEnd.Invoke();
        }
    }
    [SerializeField] private UnityEvent OnInteracted = new();
    [SerializeField] private UnityEvent OnHover = new();
    [SerializeField] private UnityEvent OnHoverEnd = new();
}
