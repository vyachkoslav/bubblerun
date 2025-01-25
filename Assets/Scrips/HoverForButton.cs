using System;
using UnityEngine;

public class HoverForButton : MonoBehaviour
{
    [SerializeField] private BetterButton button;
    [SerializeField] private SpriteRenderer buttonSprite;

    private void Awake()
    {
        button.OnHover.AddListener(() => buttonSprite.enabled = true);
        button.OnHoverEnd.AddListener(() => buttonSprite.enabled = false);
    }
}
