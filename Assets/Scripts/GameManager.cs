using UnityEngine;
using UnityEngine.SceneManagement;


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
        SceneManager.LoadScene(sceneName);
    }
}