using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] ApplicationManager _manager;
    [SerializeField] CameraFollowTargets _cameraScript;
    [SerializeField] GameObject _playerObject;
    [SerializeField] UnityEvent OnRoundEnd;

    RoundResult[] results;

    int round = -1;

    bool timerPaused = true;

    private Vector2 playerStartPos;

    private Coroutine cr;

    private void Start()
    {
        results = new RoundResult[] { new(), new() };
        playerStartPos = _playerObject.transform.position;
    }

    private void Update()
    {
        if (timerPaused ||round < 0) return;

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

        timerPaused = true;
        results[round].win = win;
        _cameraScript.useFollow = false;
        cr = StartCoroutine(DelayedReset());
        Debug.Log($"End of round {round + 1}:\nScore: {results[round].score}\nTime: {results[round].timer}\nWin: {results[round].win}");
    }

    private void EndGame()
    {
        Debug.Log($"Winner: Player {FindWinner() + 1}");
        _manager.DelayedSetScene("Menu");
    }

    private int FindWinner()
    {
        if(results[0].win ^ results[1].win)
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
        yield return new WaitForSeconds(2);
        if (round == 1)
        {
            EndGame();
        }
        else
        {
            _cameraScript.useFollow = true;
            _playerObject.transform.position = playerStartPos;
            _playerObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            OnRoundEnd.Invoke(); 
        }

        cr = null;
    }
}
