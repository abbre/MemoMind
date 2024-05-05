using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class WhiteBG : MonoBehaviour
{
    public SpriteRenderer whiteScreen;

    public float fadeDuration = 1.0f; // 渐变时间

    // Start is called before the first frame update
    public UnityEvent LoadScene;
   
    public void startWhiteScreen()
    {
        StartCoroutine(TurnWhiteScreen());
    }

    private IEnumerator TurnWhiteScreen()
    {
        float startTime = Time.time;
        Color startColor = whiteScreen.color;
        Color targetColor = new Color(1f, 1f, 1f, 1f); // 完全不透明的白色

        while (Time.time - startTime < fadeDuration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            whiteScreen.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // 设置 Image 的颜色为完全不透明的白色
        whiteScreen.color = targetColor;
        yield return new WaitForSeconds(5f);
        LoadScene.Invoke();
    }
}