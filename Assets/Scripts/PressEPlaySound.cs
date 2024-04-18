using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEPlaySound : MonoBehaviour
{
    public AudioClip soundClip; // 需要播放的声音
    private AudioSource audioSource; // AudioSource 组件
    private bool _isPlayed = false;

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        // 设置 AudioClip
        audioSource.clip = soundClip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& !_isPlayed)
        {
            // 播放声音
            audioSource.PlayOneShot(soundClip);
            _isPlayed = true;
        }
    }
}