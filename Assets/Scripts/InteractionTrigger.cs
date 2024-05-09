using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class InteractionTrigger : MonoBehaviour
{
    public bool readyToTrigger = false;

    [Header("Children")] [SerializeField] [CanBeNull]
    private GameObject _EIcon;

    [Header("Unity Event")] public UnityEvent onActivate;
    public UnityEvent mainInteraction;

    [Space(10)] [SerializeField] private bool triggerNextEventAfterAudioWithoutSubtitle;
    [SerializeField] private bool triggerNextEventAfterSubtitle;
    [SerializeField] private bool triggerNextEventAfterAnimation;
    [SerializeField] private bool triggerNextEventAfterPressE;
    public UnityEvent triggerNextInteraction;


    private bool _onActivated = false;


    [Header("EventComponents for Main Interaction")] [Space(10)] [Header("Monologue")] [CanBeNull]
    public GameObject PlayerText;


    public Camera mainCamera;
    public float interactionDistance = 3.0f;
    private Collider _collider;

    private bool _Eshowed = false;
    public bool hasInteracted = false;

    private bool _eventTrigger = false;

    public FirstPersonController firstPersonController;
    private GameObject step;
    private AudioSource stepAudio;

    [Header("Audios")] [SerializeField] private bool needAudioWithoutSubtitle;
    public bool banMovementDuringAudio = false;
    [CanBeNull] public AudioSource AudioSource;
    [CanBeNull] public AudioSource AudioSourceToStop;
    [CanBeNull] public AudioClip AudioToPlay;
    [CanBeNull] public AudioClip AudioToStop;

    [Header("Subtitles")] [SerializeField] private bool needSubtitle;
    public bool banMovementDuringSubtitle = false;
    public bool banCameraRotationDuringSubtitle = false;
    [CanBeNull] [SerializeField] private Subtitle subtitle;
    private bool _nextInteractionTriggered = false;

    [Header("Doors")] [CanBeNull] [SerializeField]
    private bool enableDoorAfterThisInteraction = false;

    [HideInInspector] public bool enableDoorInteraction = false;

    [CanBeNull] [Header("Animation")] public Animator animator;

    // Start is called before the first frame update


    void Start()
    {
        _collider = GetComponent<Collider>();
        if (_EIcon)
            _EIcon.SetActive(false);
        step = GameObject.Find("Step");
        stepAudio = step.GetComponent<AudioSource>();
    }

    public void SetReadyToTrigger()
    {
        readyToTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToTrigger) return;

        if (!_onActivated)
        {
            onActivate?.Invoke();
            _onActivated = true;
        }

        //Debug.Log("hit!");
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // 如果射线击中了可以交互的物体
            if (hit.collider == _collider && !hasInteracted)
            {
                if (!_Eshowed)
                {
                    if (_EIcon)
                        _EIcon.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _eventTrigger = true;

                    if (_EIcon)
                        _EIcon.SetActive(false);

                    _Eshowed = true;
                    hasInteracted = true;

                    if (triggerNextEventAfterPressE)
                    {
                        triggerNextInteraction?.Invoke();
                    }

                    if (enableDoorAfterThisInteraction) //如果该事件为门的触发事件
                    {
                        enableDoorInteraction = true; //该事件过后可以开门
                    }
                }
            }
            else
            {
                // 如果射线没有击中任何物体，隐藏交互图标
                if (_EIcon)
                    _EIcon.SetActive(false);

                _Eshowed = false;
            }

            if (_eventTrigger) //TODO: replace this
            {
                mainInteraction?.Invoke();
                if (AudioSourceToStop != null)
                {
                    AudioSourceToStop.clip = AudioToStop;
                    AudioSourceToStop.Stop();
                }

                if (animator)
                    animator.SetTrigger("ExampleName");

                if (PlayerText)
                    PlayerText.SetActive(true);

                if (needAudioWithoutSubtitle)
                {
                    print("Start to play audio on " + gameObject.name);
                    AudioSource.clip = AudioToPlay;
                    AudioSource.Play();

                    if (triggerNextEventAfterAudioWithoutSubtitle)
                        StartCoroutine(WaitForSetOtherTrigger(AudioSource.clip.length));
                }

                if (needSubtitle)
                    subtitle.ActivateSubtitle();


                _eventTrigger = false;
            }

            //控制玩家在音频或者字幕时候的移动
            if (needAudioWithoutSubtitle)
            {
                if (AudioSource.isPlaying && banMovementDuringAudio)
                {
                    if (banCameraRotationDuringSubtitle)
                        firstPersonController.cameraCanMove = false;

                    firstPersonController.playerCanMove = false;
                    stepAudio.enabled = false;
                }
                else
                {
                    firstPersonController.cameraCanMove = true;
                    firstPersonController.playerCanMove = true;
                    stepAudio.enabled = true;
                }
            }

            if (needSubtitle)
            {
                if (subtitle.allClipsPlayed)
                {
                    subtitle.enabled = false;
                    if (triggerNextEventAfterSubtitle && !_nextInteractionTriggered)
                    {
                        triggerNextInteraction?.Invoke();
                        _nextInteractionTriggered = true;
                    }

                    if (banMovementDuringSubtitle && subtitle.firstAudioPlayed)
                    {
                        firstPersonController.playerCanMove = true;
                        stepAudio.enabled = true;
                        enabled = false;
                    }
                }
                else
                {
                    if (banMovementDuringSubtitle && subtitle.firstAudioPlayed)
                    {
                        firstPersonController.playerCanMove = false;
                        stepAudio.enabled = false;
                    }
                }
              
            }
        }
    }

    private IEnumerator WaitForSetOtherTrigger(float waitTime)
    {
        print("Start to Wait for " + waitTime + "s on " + gameObject.name);
        yield return new WaitForSeconds(waitTime);
        triggerNextInteraction?.Invoke();
    }
}