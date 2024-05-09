using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class FadeText : MonoBehaviour
{
    [SerializeField] private bool fadeAtBeginning;
    public TextMeshProUGUI fadeText;
    [SerializeField] private float targetOpacity;
    public float fadeDuration = 1.0f; // 渐变时间
    public UnityEvent LoadScene;
    public UnityEvent TriggerNextObject;

    void Start()
    {
        if (fadeAtBeginning)
            StartCoroutine(TextFade());
    }

    public void StartTextFade()
    {
        StartCoroutine(TextFade());
    }

    private IEnumerator TextFade()
    {
        float startTime = Time.time;
        float duration = fadeDuration; // 使用fadeDuration而不是3f
        Color startColor = fadeText.color; // 获取初始颜色
        float startOpacity = startColor.a; // 获取初始透明度

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = fadeText.color; // 获取当前颜色
            newColor.a = Mathf.Lerp(startOpacity, targetOpacity, t); // 插值计算透明度
            fadeText.color = newColor; // 应用新颜色
            yield return null;
        }

        TriggerNextObject.Invoke();
        yield return new WaitForSeconds(1.0f);
        LoadScene.Invoke();
    }
}