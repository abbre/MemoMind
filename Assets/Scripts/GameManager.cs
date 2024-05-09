using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public static Camera Camera;
    [CanBeNull] public FirstPersonController firstPersonController;
    [CanBeNull] [SerializeField] private AudioSource stepAudio;
    

    private void OnEnable()
    {
        Time.timeScale = 1f;
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BanPlayerMovement()
    {
        firstPersonController.playerCanMove = false;
        firstPersonController.cameraCanMove = false;
    }

    public void BanStepAudio()
    {
        stepAudio.enabled = false;
    }
}