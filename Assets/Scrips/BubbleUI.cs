using UnityEngine;
using UnityEngine.Events;

public class BubbleUI : MonoBehaviour
{
    private int hovered = 0;

    public void Interact()
    {
        OnInteracted.Invoke();
    }
    
    public void Hover()
    {
        hovered += 1;
        if(hovered == 1)
            OnHover.Invoke();
    }

    public void UnHover()
    {
        Debug.Assert(hovered == 0);
        hovered -= 1;
        if(hovered == 0)
            OnHoverEnd.Invoke();
    }
    [SerializeField] private UnityEvent OnInteracted = new();
    [SerializeField] private UnityEvent OnHover = new();
    [SerializeField] private UnityEvent OnHoverEnd = new();
}
