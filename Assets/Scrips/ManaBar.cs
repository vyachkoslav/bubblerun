using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] TrapPlayerState trapPlayerState;
    [SerializeField] private Image bar;
    [SerializeField] private Image overlay;
    [SerializeField] private float maxOverlayAlpha;
    [SerializeField] private float minOverlayAlpha;
    [SerializeField] private float overlayAlphaChangeRate = 0.1f;
    private bool decreasing = false;
    
    public void Update()
    {
        bar.fillAmount = trapPlayerState.CurrentMana / trapPlayerState.MaxMana;
        overlay.fillAmount = (trapPlayerState.HoveredTrap?.ManaCost ?? 0) / trapPlayerState.MaxMana;
        var color = overlay.color;
        var delta = overlayAlphaChangeRate * Time.deltaTime;
        if (decreasing)
            delta = -delta;
        color.a = color.a + delta;
        var max = maxOverlayAlpha / 255f;
        var min = minOverlayAlpha / 255f;
        if (color.a > max)
        {
            color.a = max;
            decreasing = true;
        }
        else if (color.a < min)
        {
            color.a = min;
            decreasing = false;
        }
        overlay.color = color;
    }
}
