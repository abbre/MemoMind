using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioClip;

    public float delayTime;
    // Start is called before the first frame update


    // Update is called once per frame
    public void startCounting()
    {
        StartCoroutine(DelayBeforeAudioPlays());
    }

    private IEnumerator DelayBeforeAudioPlays()
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
