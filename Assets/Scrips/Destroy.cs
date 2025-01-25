using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DisableGameObject(Component obj)
    {
        obj.gameObject.SetActive(false);
    }
    public void DestroyGameObject(Component obj)
    {
        Destroy(obj.gameObject);
    }

    public void DestroyObj(Object obj)
    {
        Destroy(obj);
    }
}
