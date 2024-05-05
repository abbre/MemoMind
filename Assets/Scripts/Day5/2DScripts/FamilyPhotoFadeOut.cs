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

    public void FamilyPhotoStartFading()
    {
        StartCoroutine(FamilyPhotoFade());
    }

    private IEnumerator FamilyPhotoFade()
    {
        var time = 0f;
        while(time < fadeOutTime)
        {
            time += Time.deltaTime;
            familyPhotoMaterial.color = Color.Lerp(familyPhotoMaterial.color, Color.clear, time/fadeOutTime);
            yield return null;
        }
    }
}