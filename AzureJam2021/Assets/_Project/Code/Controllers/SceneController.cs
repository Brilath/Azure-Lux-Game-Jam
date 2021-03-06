using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Public Methods
    public static void LoadScene(string name)
    {
        Debug.Log($"You are loading scene {name}");
        SceneManager.LoadScene(name);
    }
    #endregion
}
