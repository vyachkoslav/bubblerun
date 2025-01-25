using System;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerCollided;
    [SerializeField] private AudioClip wallCollided;
    [SerializeField] private float delayBetweenSounds = 0.3f;
    private double lastPlayerCollisionTime = double.MinValue;
    private double lastWallCollisionTime = double.MinValue;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && lastPlayerCollisionTime + delayBetweenSounds < Time.time)
        {
            audioSource.PlayOneShot(playerCollided);
            lastPlayerCollisionTime = Time.time;
        }
        else if (other.gameObject.CompareTag("Environment") && lastWallCollisionTime + delayBetweenSounds < Time.time)
        {
            audioSource.PlayOneShot(wallCollided);
            lastWallCollisionTime = Time.time;
        }
    }
}
