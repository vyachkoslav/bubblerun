using UnityEngine;

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

    [SerializeField] GameObject _playerObject;

    RoundResult[] results;

    int round = -1;

    bool timerPaused = true;

    private Vector2 playerStartPos;

    private void Start()
    {
        StartRound();

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

        timerPaused = true;
        results[round].win = win;
        _playerObject.transform.position = playerStartPos;
        _playerObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        Debug.Log($"End of round {round + 1}:\nScore: {results[round].score}\nTime: {results[round].timer}\nWin: {results[round].win}");
    }

    public int FindWinner()
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
}
