using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleSettingsSetter : MonoBehaviour
{
    [SerializeField] private BubblePlayerSettings bubblePlayerSettings;
    [SerializeField] private RotateSprite rotateSprite;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject bubblePlayer;
    [SerializeField] private bool setReadyOnAction;
    
    public void OnValidate()
    {
        //rotateSprite.Init(bubblePlayerSettings.PlayerSprite, bubblePlayerSettings.AltSprite);
        playerInput.actions = bubblePlayerSettings.InputAsset;
        EditorUtility.SetDirty(rotateSprite);
        EditorUtility.SetDirty(playerInput);
    }

    private void Awake()
    {
        if (setReadyOnAction) return;
        bubblePlayer.SetActive(bubblePlayerSettings.IsReady);
        bubblePlayerSettings.IsReady = false;
    }

    public void SetReady()
    {
        if(setReadyOnAction)
            bubblePlayerSettings.IsReady = true;
    }
}
