using UnityEngine;

public class ChangeEnvironmentColor : MonoBehaviour
{
    public float colorChangeSpeed = 0.1f; // 颜色改变的速度
    public Color targetColor = Color.red; // 目标颜色

    private void Update()
    {
        // 获取场景中所有的渲染器
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        // 逐渐改变每个渲染器的颜色
        foreach (Renderer renderer in renderers)
        {
            // 获取当前材质的颜色
            Color currentColor = renderer.material.color;

            // 使用Lerp函数逐渐将当前颜色改变为目标颜色
            Color newColor = Color.Lerp(currentColor, targetColor, colorChangeSpeed * Time.deltaTime);

            // 将新的颜色应用到材质
            renderer.material.color = newColor;
        }
    }
}