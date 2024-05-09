using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Assertions.Must;

public class FadeImage : MonoBehaviour
{
    public SceneSwitcher SceneSwitcher;
    public Image image;
    public float fadeDuration = 10f; // 渐变持续时间
    public float switchDuration = 15f;

    private float _timeCnt = 0;
    private bool stateAlreadyChanged = false;
    void Start()
    {
        // 将图片的透明度设置为0
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        // 开始透明度渐变动画
        image.DOFade(1f, fadeDuration);
    }

    private void Update()
    {
        if(!stateAlreadyChanged)
        {
            _timeCnt += Time.deltaTime;
            if(_timeCnt >= switchDuration)
            {
                stateAlreadyChanged = true;
                SceneSwitcher.switchScene = true;
                enabled = false;
            }
        }
    }
}