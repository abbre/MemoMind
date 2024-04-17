using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // 目标场景的名称

    // 在其他脚本中将其设置为真以触发场景切换
    public bool switchScene = false;

    private void Update()
    {
        // 如果 switchScene 被设置为真，切换到目标场景
        if (switchScene)
        {
            // 使用异步加载场景，这样可以避免游戏卡顿
            SceneManager.LoadSceneAsync(targetSceneName);
            // 重置 switchScene 以避免反复切换
            switchScene = false;
        }
    }
}
