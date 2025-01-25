using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField] int _countdownDuration;
    [SerializeField] TextMeshProUGUI CountdownText;

    public UnityEvent OnCountDownStart;
    public UnityEvent OnCountDownEnd;

    Coroutine cr;

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (cr != null) StopCoroutine(cr);
        cr = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        OnCountDownStart.Invoke();
        Time.timeScale = 0;
        int count = _countdownDuration;
        while (count > 0)
        {
            CountdownText.text = count.ToString();
            count--;
            yield return new WaitForSecondsRealtime(1);
        }

        Time.timeScale = 1;
        CountdownText.text = "GO!";
        OnCountDownEnd.Invoke();

        yield return new WaitForSecondsRealtime(1);

        CountdownText.text = string.Empty;
        cr = null;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
