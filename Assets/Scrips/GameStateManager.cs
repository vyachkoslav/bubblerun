using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private List<Transform> players = new();
    [SerializeField] private UnityEvent OnBubblesLost = new();
    [SerializeField] private UnityEvent OnBubblesWon = new();
    private bool isEnded = false;

    void Update()
    {
        if (isEnded) return;
        
        int alivePlayers = 0;
        foreach (var player in players)
        {
            if (player != null && player.gameObject.activeSelf)
                alivePlayers++;
        }

        if (alivePlayers == 0)
        {
            OnBubblesLost.Invoke();
            isEnded = true;
        }
    }

    public void BubblesWin()
    {
        OnBubblesWon.Invoke();
    }
}
