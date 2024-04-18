using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneAudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // 存储要播放的音频
    public string[] subtitles; // 存储要显示的字幕
    public TextMeshProUGUI subtitleText; // 用于显示字幕的 TextMeshProUGUI 对象

    private AudioSource audioSource; // 用于播放音频的 AudioSource
    private int currentClipIndex = 0; // 当前播放的音频索引

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 重置当前音频索引为0，以确保每次游戏开始时都从第一个音频开始播放
        currentClipIndex = 0;

        // 播放第一个音频和相应的字幕
        PlayNextClip();
    }

    void Update()
    {
        // 检测当前音频是否播放完毕，如果播放完毕，则播放下一个音频和相应的字幕
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    // 播放下一个音频和相应的字幕
    void PlayNextClip()
    {
        // 如果当前音频索引超出了音频数组的长度，则重新开始
        if (currentClipIndex >= audioClips.Length)
        {
            currentClipIndex = 0;
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

    // 当场景被加载时调用
    void OnEnable()
    {
        // 监听场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 当场景被销毁时调用
    void OnDisable()
    {
        // 取消监听场景加载完成事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 当场景加载完成时调用
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 在场景加载完成后重新开始播放音频
        Start();
    }
}