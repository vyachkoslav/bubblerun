using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapPlayerState", menuName = "Scriptable Objects/TrapPlayerState")]
public class TrapPlayerState : ScriptableObject
{
    [SerializeField] private float maxMana = 100;
    [SerializeField] private float initialMana = 100;
    public float CurrentMana { get; private set; } = 100;

    private void OnEnable()
    {
        CurrentMana = initialMana;
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
