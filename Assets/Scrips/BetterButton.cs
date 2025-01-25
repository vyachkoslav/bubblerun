using System;
using UnityEngine;
using UnityEngine.Events;

public class BetterButton : MonoBehaviour
{
    public UnityEvent OnClick = new();
    public UnityEvent OnHover = new();
    public UnityEvent OnHoverEnd = new();

    private void OnMouseDown()
    {
        OnClick.Invoke();
    }

    private void OnMouseEnter()
    {
        OnHover.Invoke();
    }

    private void OnMouseExit()
    {
        OnHoverEnd.Invoke();
    }
}
