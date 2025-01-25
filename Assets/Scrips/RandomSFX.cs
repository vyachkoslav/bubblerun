using System.Collections.Generic;
using UnityEngine;

public class RandomSFX : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSFX()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
