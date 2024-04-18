using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class SceneAudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // 存储要播放的音频
    public string[] subtitles; // 存储要显示的字幕
    public TextMeshProUGUI subtitleText; // 用于显示字幕的 TextMeshProUGUI 对象

    private AudioSource audioSource; // 用于播放音频的 AudioSource
    private int currentClipIndex = 0; // 当前播放的音频索引
    private bool allClipsPlayed = false; // 标记所有音频是否已经播放完毕

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
        if (!audioSource.isPlaying && !allClipsPlayed)
        {
            PlayNextClip();
        }
        // 如果所有音频都已经播放完毕，则等待三秒后切换场景
        else if (allClipsPlayed)
        {
            StartCoroutine(LoadNextSceneAfterDelay(3f));
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

    // 加载下一个场景的协程
    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        // 等待指定的时间
        yield return new WaitForSeconds(delay);

        // 切换到下一个场景
        SceneManager.LoadScene("ForestPark"); // 替换 "YourNextSceneName" 为你想要加载的下一个场景的名称
    }
}