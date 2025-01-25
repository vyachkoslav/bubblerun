using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class ApplicationManager : MonoBehaviour
{
    public float delay = 0.5f;

    public new void DestroyObject(Object obj)
    {
        Destroy(obj);
    }
    
    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DelayedSetScene(string sceneName)
    {
        StartCoroutine(RunDelayed(() => SetScene(sceneName)));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DelayedQuit()
    {
        StartCoroutine(RunDelayed(Quit));
    }

    private IEnumerator RunDelayed(Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
