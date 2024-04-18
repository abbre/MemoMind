using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionTrigger : MonoBehaviour
{
    public bool readyToTrigger = false;

    [Header("Children")] [SerializeField] private GameObject _EIcon;

    [Header("Unity Event")] public UnityEvent onActivate;
    public UnityEvent mainInteraction;
    public UnityEvent triggerNextInteraction;
    [Space(10)] [SerializeField] private bool triggerEventAfterAudio;
    [SerializeField] private bool triggerEventAfterAnimation;

    public bool banMovementDuringAudio = false;
    private bool _onActivated = false;


    [Header("EventComponents")] [CanBeNull]
    public Animator animator;

    [CanBeNull] public AudioSource AudioSource;
    [CanBeNull] public AudioSource AudioSourceToStop;


    [CanBeNull] public AudioClip AudioToPlay;
    [CanBeNull] public AudioClip AudioToStop;

    [CanBeNull] public GameObject PlayerText;

    public Camera mainCamera;
    public float interactionDistance = 3.0f;
    private Collider _collider;

    private bool _Eshowed = false;
    private bool _hasInteracted = false;

    private bool _eventTrigger = false;

    public FirstPersonController firstPersonController;
    private GameObject step;
    private AudioSource stepAudio;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
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
        if (readyToTrigger)
        {
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
                if (hit.collider == _collider && !_hasInteracted)
                {
                    if (!_Eshowed)
                    {
                        _EIcon.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _eventTrigger = true;
                        _EIcon.SetActive(false);
                        _Eshowed = true;
                        _hasInteracted = true;
                    }
                }
                else
                {
                    // 如果射线没有击中任何物体，隐藏交互图标
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
                    
                    if (AudioSource != null)
                    {
                        print("Start to play audio on " + gameObject.name);
                        AudioSource.clip = AudioToPlay;
                        AudioSource.Play();
                        if (triggerEventAfterAudio)
                            StartCoroutine(WaitForSetOtherTrigger(AudioSource.clip.length));
                    }

                    _eventTrigger = false;
                }
                if (AudioSource != null)
                {
                    if (AudioSource.isPlaying && banMovementDuringAudio)
                    {
                        firstPersonController.playerCanMove = false;
                        stepAudio.enabled = false;
                    }
                    else
                    {
                        firstPersonController.playerCanMove = true;
                        stepAudio.enabled = true;
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