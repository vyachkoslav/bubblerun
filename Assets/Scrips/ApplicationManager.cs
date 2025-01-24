using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    public float delay = 0.5f;
    [SerializeField] private BubbleUIInteraction bubble;
    
    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DelayedSetScene(string sceneName)
    {
        Destroy(bubble.gameObject);
        StartCoroutine(RunDelayed(() => SetScene(sceneName)));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DelayedQuit()
    {
        Destroy(bubble.gameObject);
        StartCoroutine(RunDelayed(Quit));
    }

    private IEnumerator RunDelayed(Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
