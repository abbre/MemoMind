using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenEffects : MonoBehaviour
{
    public Image whiteImage; // 白色图片
    public float fadeSpeed = 0.5f; // 变白速度
    public float delayBeforeFade = 6f; // 在场景开始后多长时间开始变白
    public Sprite[] photos; // 照片数组
    public float photoDisplayTime = 2f; // 每张照片的显示时间

    private bool isFading = false; // 是否正在变白
    private float alpha = 0f; // 初始透明度

    private void Start()
    {
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        isFading = true;

        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            whiteImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        alpha = 1f;

        // 白屏后，开始播放照片
        yield return new WaitForSeconds(1f); // 可以在这里加一些额外的延迟

        foreach (Sprite photo in photos)
        {
            yield return DisplayPhoto(photo);
            yield return new WaitForSeconds(photoDisplayTime);
        }

        // 播放完毕后，开始渐出
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            whiteImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        alpha = 0f;
        isFading = false;
    }

    private IEnumerator DisplayPhoto(Sprite photo)
    {
        // 显示照片
        whiteImage.sprite = photo;
        Debug.Log("Displaying photo: " + photo.name);
        yield return null;
    }
}
