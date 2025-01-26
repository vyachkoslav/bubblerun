using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BubbleSettingsSetter : MonoBehaviour
{
    [SerializeField] private BubblePlayerSettings bubblePlayerSettings;
    [SerializeField] private RotateSprite rotateSprite;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject bubblePlayer;
    [SerializeField] private bool setReadyOnAction;

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (playerInput.actions != bubblePlayerSettings.InputAsset)
        {
            playerInput.actions = bubblePlayerSettings.InputAsset;
            EditorUtility.SetDirty(playerInput);
        }
    }
#endif

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            bubblePlayerSettings.IsReady = false;
        }
        if (setReadyOnAction) return;
        bubblePlayer.SetActive(bubblePlayerSettings.IsReady);
    }

    public void SetReady()
    {
        if (setReadyOnAction)
            bubblePlayerSettings.IsReady = true;
    }
}