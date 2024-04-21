using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.Events;


public class Subtitle : MonoBehaviour
{
    public AudioClip[] audioClips; // 存储要播放的音频
    public string[] subtitles; // 存储要显示的字幕
    public TextMeshProUGUI subtitleText; // 用于显示字幕的 TextMeshProUGUI 对象

    [SerializeField] private AudioSource audioSource; // 用于播放音频的 AudioSource
    private int currentClipIndex = 0; // 当前播放的音频索引
    [HideInInspector] public bool allClipsPlayed = false; // 标记所有音频是否已经播放完毕
    [HideInInspector] public bool firstAudioPlayed = false;


    [SerializeField] private bool subtitlePlayAtBeginning;
    [SerializeField] private float timeBeforeFirstAudioIsPlayed;

    public UnityEvent afterSubtitle;
    private bool _triggeredAfterSubtitleEvent = false;

    [SerializeField] private bool enableDoorInteractionAfterSubtitle;
    [HideInInspector] public bool enableDoorInteraction = false;

  
    void Start()
    {
        // 重置当前音频索引为0，以确保每次游戏开始时都从第一个音频开始播放
        currentClipIndex = 0;


        if (subtitlePlayAtBeginning)
            ActivateSubtitle();
    }


    public void ActivateSubtitle()
    {
        if (timeBeforeFirstAudioIsPlayed > 0f)
        {
            StartCoroutine(CountDownPlayFirstAudio());
        }
        else
        {
            timeBeforeFirstAudioIsPlayed = 0f;
            StartCoroutine(CountDownPlayFirstAudio());
            PlayNextClip();
        }
    }


    private IEnumerator CountDownPlayFirstAudio()
    {
        yield return new WaitForSeconds(timeBeforeFirstAudioIsPlayed);
        PlayNextClip();
        firstAudioPlayed = true;
    }

    void Update()
    {
        if (firstAudioPlayed)
        {
            // 检测当前音频是否播放完毕，如果播放完毕，则播放下一个音频和相应的字幕
            if (!audioSource.isPlaying && !allClipsPlayed)
            {
                PlayNextClip();
            }

            if (allClipsPlayed)
            {
                subtitleText.text = "";
                if (enableDoorInteractionAfterSubtitle)
                    enableDoorInteraction = true;
                if (!_triggeredAfterSubtitleEvent)
                {
                        afterSubtitle.Invoke();
                    _triggeredAfterSubtitleEvent = true;
                }
            }
        }
    }

    // 播放下一个音频和相应的字幕
    void PlayNextClip()
    {
        // 如果当前音频索引超出了音频数组的长度，则重新开始
        if (currentClipIndex >= audioClips.Length)
        {
            currentClipIndex = 0;
            allClipsPlayed = true; // 设置标志为 true，表示所有音频已经播放完毕
            return;
        }

        // 设置 AudioSource 的音频剪辑为当前索引对应的音频
        audioSource.clip = audioClips[currentClipIndex];
        // 播放音频
        audioSource.Play();

        // 显示相应的字幕
        if (subtitleText != null && currentClipIndex < subtitles.Length)
        {
            subtitleText.text = subtitles[currentClipIndex];
        }

        // 增加音频索引以准备播放下一个音频
        currentClipIndex++;
    }
}