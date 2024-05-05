using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private bool fadeAtBeginning;

    public Image blackScreenImage;
    [SerializeField] private float targetOpacity;
    public float fadeDuration = 1.0f; // 渐变时间

    // Start is called before the first frame update
    public UnityEvent LoadScene;
    [CanBeNull] public AudioSource audioSource;
    [CanBeNull] public AudioClip audioClip;

    public UnityEvent TriggerNextObject;


    void Start()
    {
        if (fadeAtBeginning)
            StartCoroutine(TurnBlackScreen());
    }

    public void startBlackScreen()
    {
        StartCoroutine(TurnBlackScreen());
    }

    private IEnumerator TurnBlackScreen()
    {
        float startTime = Time.time;
        float duration = 3f; // 渐变时间
        float startOpacity = blackScreenImage.color.a;


        if (audioSource)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = blackScreenImage.color;
            newColor.a = Mathf.Lerp(startOpacity, targetOpacity, t);
            blackScreenImage.color = newColor;
            yield return null;
        }

        TriggerNextObject.Invoke();
        yield return new WaitForSeconds(1.0f);
        LoadScene.Invoke();
    }
}