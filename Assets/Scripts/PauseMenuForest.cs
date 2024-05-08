using JetBrains.Annotations;
using UnityEngine;

public class PauseMenuForest : MonoBehaviour
{
    public GameObject pauseMenuUI;
    // [CanBeNull] public FirstPersonController firstPersonController;
   public static bool isPaused = false;

    // [SerializeField] private SceneAudioManager sceneAudioManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        // firstPersonController.cameraCanMove = false;
        // sceneAudioManager.PauseAudio(false);
        Time.timeScale = 0f; // 暂停游戏
        Cursor.lockState = CursorLockMode.None; // 解锁光标
        Cursor.visible = true; // 显示光标
        pauseMenuUI.SetActive(true); // 显示暂停菜单
    }

    public void ResumeGame()
    {
        // firstPersonController.cameraCanMove = true;
        isPaused = false;
        // sceneAudioManager.PauseAudio(true);
        Time.timeScale = 1f; // 恢复游戏
        Cursor.lockState = CursorLockMode.Locked; // 锁定光标
        Cursor.visible = false; // 隐藏光标
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
    }

    public void ReturnToMainMenu()
    {
        // 在这里编写返回主菜单的逻辑，比如加载主菜单场景等
        Debug.Log("Returning to main menu...");
    }
}