using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private TrapPlayerState trapPlayerState;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _manaCost;
    public float ManaCost => _manaCost;
    public float Cooldown => _cooldown;
    private double lastActivation = double.MinValue;
    protected abstract void ActivateTrap();
    protected abstract bool IsRunning();

    public void Trigger()
    {
        if (IsRunning() || lastActivation + _cooldown > Time.timeAsDouble)
        {
            trapPlayerState.OnNotReady.Invoke();
            return;
        }
        if (!trapPlayerState.TryUseMana(_manaCost))
        {
            trapPlayerState.OnNotEnoughMana.Invoke();
            return;
        }
        lastActivation = Time.timeAsDouble;
        ActivateTrap();
    }
}
