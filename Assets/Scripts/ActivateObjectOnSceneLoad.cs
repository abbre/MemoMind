using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateObjectOnSceneLoad : MonoBehaviour

{
    public GameObject objectToActivate; // 要激活的物体
    public string sceneName;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("you have successfully started the game");
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("OnSceneLoaded Func: scene " + scene.name);
        if (scene.name == sceneName)
        {
            objectToActivate.SetActive(true);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Debug.Log("you have successfully loaded this scene");
        }
    }
}