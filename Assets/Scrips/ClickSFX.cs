using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickSFX : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
