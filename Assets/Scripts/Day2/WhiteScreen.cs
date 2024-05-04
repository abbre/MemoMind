using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WhiteScreen : MonoBehaviour
{
    public Image whiteScreenImage;

    public float fadeDuration = 1.0f; // 渐变时间

    // Start is called before the first frame update
    public UnityEvent LoadScene;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void startWhiteScreen()
    {
        StartCoroutine(TurnWhiteScreen());
    }

    private IEnumerator TurnWhiteScreen()
    {
        float startTime = Time.time;
        float duration = 1.0f; // 渐变时间
        Color startColor = whiteScreenImage.color;
        Color targetColor = new Color(1f, 1f, 1f, 1f); // 完全不透明的白色

        audioSource.clip = audioClip;
        audioSource.Play();

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            whiteScreenImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // 设置 Image 的颜色为完全不透明的白色
        whiteScreenImage.color = targetColor;
        yield return new WaitForSeconds(1.0f);
        LoadScene.Invoke();
    }
}