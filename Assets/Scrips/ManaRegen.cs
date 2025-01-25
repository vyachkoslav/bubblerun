using UnityEngine;

public class ManaRegen : MonoBehaviour
{
    [SerializeField] private TrapPlayerState trapPlayerState;
    [SerializeField] private float manaInSecond;
    void Update()
    {
        trapPlayerState.AddMana(manaInSecond * Time.deltaTime);
    }
}
