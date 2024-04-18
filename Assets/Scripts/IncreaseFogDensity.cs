using UnityEngine;

public class IncreaseFogDensity : MonoBehaviour
{
    public float increaseSpeed = 0.1f; // 增加速度
    public SceneSwitcher sceneSwitcher;
    private void Update()
    {
        RenderSettings.fogDensity += increaseSpeed * Time.deltaTime;

        // 如果雾的密度达到了1，停止增加
        if (RenderSettings.fogDensity >= 1f)
        {
            RenderSettings.fogDensity = 1f;
            sceneSwitcher.switchScene = true;
            enabled = false; // 停用脚本
            
        }
    }
}