using System.Collections;
using UnityEngine;

public class AutoLoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private float _loadDelay;

    private void Start()
    {
        StartCoroutine(LoadNewSceneWithDelay(_loadDelay));
    }

    private IEnumerator LoadNewSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneController.LoadScene(_sceneName);
    }
}
