using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] ScoreTracker _scoreTrackerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _scoreTrackerScript.EndRound(true);
    }
}
