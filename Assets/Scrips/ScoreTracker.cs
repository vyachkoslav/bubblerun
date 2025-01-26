using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ScoreTracker : MonoBehaviour
{
    private class RoundResult
    {
        public float timer;
        public float score;
        public bool win;

        public RoundResult()
        {
            timer = 0f;
            score = 0f;
            win = false;
        }
    }

    [SerializeField] TextMeshProUGUI p1WinText;
    [SerializeField] TextMeshProUGUI p2WinText;
    [SerializeField] ApplicationManager _manager;
    [SerializeField] GameStateManager _gameStateManager;
    [SerializeField] CameraFollowTargets _cameraScript;
    [SerializeField] GameObject _playerObject;
    [SerializeField] UnityEvent OnRoundStart;
    [SerializeField] UnityEvent OnRoundEnd;

    RoundResult[] results;

    int round = -1;

    bool timerPaused = true;

    private Coroutine cr;

    private void Start()
    {
        ResetRounds();
    }

    private void ResetRounds()
    {
        round = -1;
        results = new RoundResult[] { new(), new() };
    }

    private void Update()
    {
        if (timerPaused || round < 0) return;

        results[round].timer += Time.deltaTime;
        results[round].score = Mathf.Max(results[round].score, _playerObject.transform.position.y);
    }

    public void StartRound()
    {
        round++;
        timerPaused = false;
    }

    public void EndRound(bool win)
    {
        if (cr != null) return;
        _gameStateManager.DisableInput();

        timerPaused = true;
        results[round].win = win;
        _cameraScript.useFollow = false;
        Debug.Log($"End of round {round + 1}:\nScore: {results[round].score}\nTime: {results[round].timer}\nWin: {results[round].win}");
        cr = StartCoroutine(DelayedReset());
    }

    private void EndGame()
    {
        int winner = FindWinner();
        Debug.Log($"Winner: Player {winner + 1}");

        if (winner == 0)
        {
            p1WinText.gameObject.SetActive(true);
        }
        else
        {
            p2WinText.gameObject.SetActive(true);
        }

        // _manager.DelayedSetScene("1v1");
        ResetRounds();
        cr = StartCoroutine(DelayedReset());
    }

    private int FindWinner()
    {
        if (results[0].win ^ results[1].win)
        {
            if (results[0].win) return 0;
            else return 1;
        }

        if (results[0].win)
        {
            if (results[0].timer < results[1].timer) return 0;
            else return 1;
        }

        if (results[0].score > results[1].score) return 0;
        else return 1;
    }

    private IEnumerator DelayedReset()
    {
        if (round == 1)
        {
            EndGame();
        }
        else
        {
            yield return new WaitForSeconds(2);
            p1WinText.gameObject.SetActive(false);
            p2WinText.gameObject.SetActive(false);

            _cameraScript.useFollow = true;
            
            OnRoundEnd.Invoke(); 
        }

        cr = null;
    }
}
