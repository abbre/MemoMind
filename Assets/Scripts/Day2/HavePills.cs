using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HavePills : MonoBehaviour
{
    public Transform eatPillTransform;
    public Transform pillPillTransform;
    public FirstPersonController firstPersonController;

    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离
    private AudioSource stepAudio;

    public float holdTime = 1.5f;

    public GameObject pill;

    public GameObject p1;
    public GameObject p2;
    private MeshRenderer pill1; // 将 pill1 和 pill2 定义为成员变量
    private MeshRenderer pill2;

    public Camera mainCamera;
    private Collider _collider;
    private bool _Eshowed = false;
    private bool hasInteractedWithPills = false;

    private AudioSource audioSource;

    private int pillsTaken = 0;
    public List<AudioClip> reminderVoices; // 存储提醒语音
    public AudioClip eat;

    public Image whiteScreenImage;
    public float fadeDuration = 3.0f; // 渐变时间

    private int currentAudioIndex = 0; // 当前音频索引
    private bool waiting = false;
    private float waitStartTime = 0f;
    private float waitDuration = 5f;
    public GameObject step;



    [SerializeField] private GameObject[] TimeToTakePillText;


    [SerializeField] private GameObject pillJar;

    [SerializeField] private bool readyToTrigger = false;

    private bool _triggerWhiteScreen = false;
    public UnityEvent LoadScene;

    public void SetReadyToTrigger()
    {
        readyToTrigger = true;
    }

    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;

        audioSource = GetComponent<AudioSource>();
        stepAudio = step.GetComponent<AudioSource>();
        pillPillTransform = pill.transform;

        // 初始化成员变量 pill1 和 pill2
        pill1 = p1.GetComponent<MeshRenderer>();
        pill2 = p2.GetComponent<MeshRenderer>();
        foreach (GameObject textObject in TimeToTakePillText)
        {
            textObject.SetActive(false);
        }
    }

    void Update()
    {
        if (readyToTrigger)
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
                            pillsTaken++;

                        if (pillsTaken < 5)
                        {
                            pill1.enabled = true;
                            pill2.enabled = true;
                            pill.transform.position = eatPillTransform.position;
                            pill.transform.rotation = eatPillTransform.rotation;
                            pill.transform.localScale = eatPillTransform.localScale;
                            StartCoroutine(PillDisappear());
                        }
                        else
                        {
                            pill1.enabled = false;
                            pill2.enabled = false;
                        }

                        interactionIcon.SetActive(false);

                        audioSource.enabled = true;

                        audioSource.clip = eat; // 将 eat AudioClip 赋值给 audioSource.clip
                        audioSource.Play();


                        firstPersonController.playerCanMove = false;
                        firstPersonController.cameraCanMove = false;
                        stepAudio.enabled = false;

                        _Eshowed = true;

                        waitStartTime = Time.time;

                        // 玩家吃药后增加药物计数

                        // 检查是否已经吃了五次药物
                        if (pillsTaken == 5)
                        {
                            StartCoroutine(takePillJar());
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
        }
    }


    private IEnumerator takePillJar()
    {
        pillJar.transform.position = eatPillTransform.position;
        yield return new WaitForSeconds(holdTime);
        pillJar.SetActive(false);
        firstPersonController.playerCanMove = true;
        firstPersonController.cameraCanMove = true;
        stepAudio.enabled = true;
        float startTime = Time.time;
        float duration = 1.0f; // 渐变时间
        Color startColor = whiteScreenImage.color;
        Color targetColor = new Color(1f, 1f, 1f, 1f); // 完全不透明的白色

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            whiteScreenImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // 设置 Image 的颜色为完全不透明的白色
        whiteScreenImage.color = targetColor;
        LoadScene.Invoke();
    }

    private IEnumerator PillDisappear()
    {
        yield return new WaitForSeconds(holdTime);

        pill1.enabled = false;
        pill2.enabled = false;
        firstPersonController.playerCanMove = true;
        firstPersonController.cameraCanMove = true;
        stepAudio.enabled = true;

        // Reset the pill's position, rotation, and scale
        pill.transform.position = pillPillTransform.position;
        pill.transform.rotation = pillPillTransform.rotation;
        pill.transform.localScale = pillPillTransform.localScale;

        audioSource.clip = reminderVoices[currentAudioIndex];
        audioSource.Play();

        foreach (GameObject textObject in TimeToTakePillText)
        {
            textObject.SetActive(false);
        }

        TimeToTakePillText[currentAudioIndex].SetActive(true);

        // 增加当前音频索引，并确保它在列表长度范围内循环
        currentAudioIndex = (currentAudioIndex + 1) % reminderVoices.Count;
    }
    
}