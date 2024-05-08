using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Wakeup : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public Camera mainCamera;
    public Camera sleepCamera;
    public Image maskImage; // 遮罩的 Image UI 元素
    [CanBeNull] public AudioClip wakeupSound; // 醒来时播放的音频4
    [SerializeField] private AudioSource stepSound;
    private bool _cameraSwitched = false;
    [SerializeField] private AudioClip footStep1;

    [SerializeField] private float fadeTime = 0.2f;
    private bool audioEnd = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeMask());
        mainCamera.enabled = false;
        firstPersonController.playerCanMove = false;
     
    }

    void Update()
    {
        if ((wakeupSound != null && audioEnd) || wakeupSound == null)
        {
            Color currentColor = maskImage.color;
            currentColor.a -= Time.deltaTime * fadeTime;
            maskImage.color = currentColor;

            if (maskImage.color.a <= 0)
            {
                maskImage.enabled = false; // 遮罩完全透明时禁用遮罩
            }
        }
    }

    IEnumerator FadeMask()
    {
        if (wakeupSound != null && audioSource != null)
        {
            audioSource.clip = wakeupSound;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length - 1);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        // 等待音频播放结束


        audioEnd = true;

        // 切换 Camera 的逻辑
        mainCamera.enabled = true;
        sleepCamera.enabled = false;


        if (!_cameraSwitched)
        {
            firstPersonController.playerCanMove = true;
            stepSound.PlayOneShot(footStep1);
            _cameraSwitched = true;
        }
    }
}