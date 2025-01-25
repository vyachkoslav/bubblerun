using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2D : MonoBehaviour
{
    public UnityEvent<Collider2D> OnEnter = new();
    public UnityEvent<Collider2D> OnExit = new();
    public UnityEvent<Collider2D> OnPlayerEnter = new();
    public UnityEvent<Collider2D> OnPlayerExit = new();
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter.Invoke(other);
        if(other.CompareTag("Player"))
            OnPlayerEnter.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        OnExit.Invoke(other);
        if(other.CompareTag("Player"))
            OnPlayerExit.Invoke(other);
    }
}
