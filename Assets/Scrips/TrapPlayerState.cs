using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapPlayerState", menuName = "Scriptable Objects/TrapPlayerState")]
public class TrapPlayerState : ScriptableObject
{
    [SerializeField] private float maxMana = 100;
    [SerializeField] private float initialMana = 100;
    public float CurrentMana { get; private set; } = 100;
    public float MaxMana => maxMana;
    public Trap HoveredTrap { get; private set; }

    private void OnEnable()
    {
        CurrentMana = initialMana;
    }

    public void SetHoveredTrap(Trap trap)
    {
        HoveredTrap = trap;
    }

    public void RemoveHoveredTrap(Trap trap)
    {
        if (HoveredTrap == trap)
            HoveredTrap = null;
    }

    public void ClearHoveredTrap()
    {
        HoveredTrap = null;
    }

    public bool TryUseMana(float mana)
    {
        if (CurrentMana <= mana)
            return false;
        CurrentMana -= mana;
        return true;
    }

    public void AddMana(float mana)
    {
        CurrentMana = Mathf.Clamp(CurrentMana + mana, 0, maxMana);
    }
}
