using System;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerCollided;
    [SerializeField] private AudioClip wallCollided;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            audioSource.PlayOneShot(playerCollided);
        else if(other.gameObject.CompareTag("Environment"))
            audioSource.PlayOneShot(wallCollided);
    }
}
