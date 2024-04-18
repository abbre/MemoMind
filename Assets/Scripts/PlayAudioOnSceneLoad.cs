using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAudioOnSceneLoad : MonoBehaviour
{
    public AudioClip audioClip; // 要播放的音频

    void Start()
    {
        // 监听场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 播放音频
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
    }

    void OnDestroy()
    {
        // 在销毁脚本时取消监听场景加载完成事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}