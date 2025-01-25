using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class BetterButton : MonoBehaviour
{
    [SerializeField] Sprite _sprite;
    [SerializeField] Sprite _clickSprite;

    public UnityEvent OnClick = new();
    public UnityEvent OnHover = new();
    public UnityEvent OnHoverEnd = new();

    private new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        renderer.sprite = _clickSprite;
        OnClick.Invoke();
    }

    private void OnMouseUp()
    {
        renderer.sprite = _sprite;
    }

    private void OnMouseEnter()
    {
        OnHover.Invoke();
    }

    private void OnMouseExit()
    {
        renderer.sprite = _sprite;
        OnHoverEnd.Invoke();
    }
}
