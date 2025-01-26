using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public List<Transform> Players = new();
    [SerializeField] private bool disableLostCondition = false;
    [SerializeField] private UnityEvent OnBubblesLost = new();
    [SerializeField] private UnityEvent OnBubblesWon = new();
    private List<Rigidbody2D> rbs = new();
    private List<PlayerInput> playerInputs = new();
    private List<Vector3> startPositions = new();
    private bool isEnded = false;

    private void Awake()
    {
        isEnded = disableLostCondition;
        Players.ForEach(p =>
        {
            startPositions.Add(p.position);
            rbs.Add(p.GetComponent<Rigidbody2D>());
            playerInputs.Add(p.GetComponent<PlayerInput>());
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (isEnded) return;
        
        int alivePlayers = 0;
        foreach (var player in Players)
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

    public void DisableInput()
    {
        playerInputs.ForEach(p=> p.enabled = false);
    }

    public void EnableInput()
    {
        playerInputs.ForEach(p=> p.enabled = true);
    }
    public void ResetAllPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].gameObject.SetActive(true);
            Players[i].transform.position = startPositions[i];
            rbs[i].linearVelocity = Vector2.zero;
            playerInputs[i].enabled = true;
        }
    }
}
