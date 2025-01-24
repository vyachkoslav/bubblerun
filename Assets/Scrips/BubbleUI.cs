using UnityEngine;
using UnityEngine.Events;

public class BubbleUI : MonoBehaviour
{
    public UnityEvent OnInteracted = new();
    public UnityEvent OnHover = new();
    public UnityEvent OnHoverEnd = new();
}
