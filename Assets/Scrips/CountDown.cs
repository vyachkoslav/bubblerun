using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CountdownText;

    [SerializeField] UnityEvent OnCountDownEnd;

    Coroutine cr;

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (cr != null) StopCoroutine(cr);
        Time.timeScale = 0;
        cr = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        int count = 3;
        while (count > 0)
        {
            CountdownText.text = count.ToString();
            count--;
            yield return new WaitForSeconds(1);
        }

        Time.timeScale = 1;
        CountdownText.text = "GO!";
        OnCountDownEnd.Invoke();

        yield return new WaitForSeconds(1);

        CountdownText.text = string.Empty;
        cr = null;
    }
}
