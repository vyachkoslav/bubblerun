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

    private void Start()
    {
        results = new RoundResult[2];
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
