using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BubblePlayerSettings", menuName = "Scriptable Objects/BubblePlayerSettings")]
public class BubblePlayerSettings : ScriptableObject
{
    public Sprite PlayerSprite;
    public Sprite AltSprite;
    public InputActionAsset InputAsset;
    [NonSerialized] public bool IsReady = false;

    public void SetReady(bool ready)
    {
        IsReady = ready;
    }
}
