using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateObjectOnSceneLoad : MonoBehaviour

{
    public GameObject objectToActivate; // 要激活的物体
    public string sceneName;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("you have successfully started the game");
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            objectToActivate.SetActive(true);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Debug.Log("you have successfully loaded this scene");
        }
    }
}