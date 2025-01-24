using UnityEngine;
using UnityEngine.Events;

public class BetterButton : MonoBehaviour
{
    public UnityEvent OnClick = new();

    private void OnMouseDown()
    {
        OnClick.Invoke();
    }

}
