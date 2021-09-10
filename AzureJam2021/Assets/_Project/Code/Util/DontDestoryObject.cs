using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"Never destroying {gameObject.name}");
        DontDestroyOnLoad(this);
    }
}
