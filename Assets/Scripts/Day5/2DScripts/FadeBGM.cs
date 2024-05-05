using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeBGM : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;
    

    [SerializeField] private float duration = 3f; // 渐变时间

    // Start is called before the first frame update
    public void StartSoundFade()
    {
        StartCoroutine(BGMFadeOut());
    }

    private IEnumerator BGMFadeOut()
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            BGM.volume = Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }
}