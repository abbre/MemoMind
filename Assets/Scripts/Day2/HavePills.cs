using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HavePills : MonoBehaviour
{
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离
    
    public static event Action EPressed;
    public Camera mainCamera;
    private Collider _collider;
    private bool _Eshowed = false;
    private bool hasInteractedWithPills = false;

    private AudioSource audioSource;

    private int pillsTaken = 0;
    public List<AudioClip> reminderVoices; // 存储提醒语音
    public AudioClip eat;

    public Image blackScreenImage; // 引用黑色的 Image 对象
    public float fadeDuration = 1.0f; // 渐变时间

    private int currentAudioIndex = 0; // 当前音频索引
    private bool waiting = false;
    private float waitStartTime = 0f;
    private float waitDuration = 5f;

    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;

        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider == _collider)
            {
                if (!_Eshowed)
                {
                    interactionIcon.SetActive(true);
                    _Eshowed = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionIcon.SetActive(false);

                    audioSource.enabled = true;

                    audioSource.clip = eat; // 将 eat AudioClip 赋值给 audioSource.clip
                    audioSource.Play();
            
                    _Eshowed = true;
                    
                    waiting = true;
                    waitStartTime = Time.time;
                    Debug.Log("5f start.");
    
                    // 玩家吃药后增加药物计数
                    pillsTaken++;
                    // 检查是否已经吃了五次药物
                    if (pillsTaken >= 5)
                    {
                        // 触发黑屏效果或其他你想要的动作
                        StartCoroutine(TriggerBlackScreen());
                    }
                }
            }
            else
            {
                // 如果射线没有击中任何物体，隐藏交互图标
                interactionIcon.SetActive(false);
                _Eshowed = false;
            }
        }

        if (waiting && (Time.time - waitStartTime >= waitDuration))
        {  
            Debug.Log("5f passed.");
            audioSource.clip = reminderVoices[currentAudioIndex];
            audioSource.Play();
                
            // 增加当前音频索引，并确保它在列表长度范围内循环
            currentAudioIndex = (currentAudioIndex + 1) % reminderVoices.Count;

            // 重置等待状态
            waiting = false;
            
        }
    }

    IEnumerator TriggerBlackScreen()
    {
        Debug.Log("Starting black screen effect.");
        float startTime = Time.time;
        float alpha = 0f;
        while (alpha < 1f)
        {
            float elapsedTime = Time.time - startTime;
            alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreenImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // 设置 Image 的颜色为完全不透明
        blackScreenImage.color = new Color(0f, 0f, 0f, 1f);
        Debug.Log("Black screen effect finished.");
    }
}
