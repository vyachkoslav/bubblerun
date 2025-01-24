using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _manaCost;
    public abstract void Trigger();
}
