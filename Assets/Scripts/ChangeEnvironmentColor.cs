using UnityEngine;

public class ChangeSkyboxColor : MonoBehaviour
{
    public float colorChangeSpeed = 0.1f; // 颜色改变的速度
    public Color targetColor = Color.red; // 目标颜色
    // public SceneSwitcher sceneSwitcher;
    public Material skyboxMaterial; // Skybox 材质
    private Color originalColor; // 初始颜色
    public SceneSwitcher sceneSwitcher;
    private void Start()
    {
        // 获取当前 Skybox 材质
        skyboxMaterial = RenderSettings.skybox;

        // 如果没有 Skybox 材质，创建一个新的 Skybox 材质并应用到渲染设置中
        if (skyboxMaterial == null)
        {
            skyboxMaterial = new Material(Shader.Find("Skybox/6 Sided"));
            RenderSettings.skybox = skyboxMaterial;
        }

        // 保存初始颜色
        originalColor = skyboxMaterial.GetColor("_Tint");
    }
    
    private void Update()
    {
        // 使用 Lerp 函数逐渐将当前颜色改变为目标颜色
        Color newColor = Color.Lerp(skyboxMaterial.GetColor("_Tint"), targetColor, colorChangeSpeed * Time.deltaTime);

        // 将新的颜色应用到 Skybox 材质
        skyboxMaterial.SetColor("_Tint", newColor);
        if (sceneSwitcher.switchScene == true)
        {
            skyboxMaterial.SetColor("_Tint", originalColor);
        }
        
    }

    // private void ()
    // {
    //     // 游戏停止时重置 Skybox 的颜色为初始颜色
    //     skyboxMaterial.SetColor("_Tint", originalColor);
    // }
}