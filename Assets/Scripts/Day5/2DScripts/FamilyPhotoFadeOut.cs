using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyPhotoFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    private Material familyPhotoMaterial;
    private Color originalColor;

    void Start()
    {
        // 获取物体的初始材质
        familyPhotoMaterial = GetComponent<Renderer>().material;
        // 存储初始颜色
        originalColor = familyPhotoMaterial.color;
    }

    void Update()
    {
        float fadeAmount = Time.deltaTime / fadeOutTime;
        familyPhotoMaterial.color = Color.Lerp(familyPhotoMaterial.color, Color.clear, fadeAmount);
    }

    // 这是一个示例方法，用于重置材质的颜色为初始颜色
    public void ResetMaterialColor()
    {
        familyPhotoMaterial.color = originalColor;
    }
}