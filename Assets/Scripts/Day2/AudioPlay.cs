using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay: MonoBehaviour
{
    public List<AudioClip> reminderVoices; // 录音列表
    public AudioSource audioSource; // 音频源

    private bool ePressed = false;

    void OnEnable()
    {
        // 监听事件
        HavePills.EPressed += EPressedHandler;
    }

    void OnDisable()
    {
        // 取消监听事件
        HavePills.EPressed -= EPressedHandler;
    }

    void EPressedHandler()
    {
        ePressed = true;
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if (ePressed)
            {
                // 播放随机录音
                int index = UnityEngine.Random.Range(0, reminderVoices.Count);
                audioSource.clip = reminderVoices[index];
                audioSource.Play();

                ePressed = false; // 重置ePressed
            }
        }
    }
}
