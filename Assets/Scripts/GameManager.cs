using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public static Camera Camera;

    private void OnEnable()
    {
        Camera = _camera;
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(AsyncLoadScene(sceneName));
    }

    private IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}