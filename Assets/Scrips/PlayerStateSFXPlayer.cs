using System;
using UnityEngine;

public class PlayerStateSFXPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TrapPlayerState trapPlayerState;
    [SerializeField] private RandomSFX clickSound;
    [SerializeField] private AudioClip noManaClip;
    [SerializeField] private AudioClip notReadyClip;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        trapPlayerState.Clicked.AddListener(clickSound.PlayRandomSFX);
        trapPlayerState.OnNotEnoughMana.AddListener(PlayNoManaClip);
        trapPlayerState.OnNotReady.AddListener(PlayNotReadyClip);
    }

    void PlayNoManaClip()
    {
        audioSource.PlayOneShot(noManaClip);
    }

    void PlayNotReadyClip()
    {
        audioSource.PlayOneShot(notReadyClip);
    }

    private void OnDestroy()
    {
        trapPlayerState.OnNotEnoughMana.RemoveListener(PlayNoManaClip);
        trapPlayerState.OnNotReady.RemoveListener(PlayNotReadyClip);
    }
}
