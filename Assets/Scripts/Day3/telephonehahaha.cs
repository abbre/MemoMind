using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    public GameObject player;
    private Vector3 originalVelocity;
    public GameObject[] disabledObjects; // 存储需要禁用的物体
    public GameObject[] enabledObjects; 
    public MonoBehaviour[] disabledScripts;
    public MonoBehaviour[] enabledScripts;

    private bool phoneRinging = false;
    private Rigidbody rb;
    public Camera mainCamera;
    public float interactionDistance = 2f;
    private Collider _collider;
    public GameObject dialogue;
    public GameObject interactionIcon;
    private bool _hasInteracted = false;
    private bool _Eshowed = false;
    public AudioSource audioSource; 


    void Start()
    {
        _collider = GetComponent<Collider>();
        rb = player.GetComponent<Rigidbody>();
        mainCamera = GameManager.Camera;
        originalVelocity = rb.velocity;
        interactionIcon.SetActive(false);
        foreach (MonoBehaviour script in disabledScripts)
        {
            // 禁用脚本
            script.enabled = false;
        }
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }
        StartCoroutine(RingPhoneAfterDelay(8f)); // 在8秒后开始响铃
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // 如果射线击中了可以交互的物体
            if (hit.collider == _collider && !_hasInteracted)
            {
                if (!_Eshowed && phoneRinging)
                {
                    interactionIcon.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionIcon.SetActive(false);
                    _Eshowed = true;
                    _hasInteracted = true;
                }
            }
            else
            {
                // 如果射线没有击中任何物体，隐藏交互图标
                interactionIcon.SetActive(false);
                _Eshowed = false;
            }
        }
        
        if (_hasInteracted)
        {
            TelephoneTime();
        }
    }

    void TelephoneTime()
    {
        dialogue.SetActive(true);
        rb.velocity = Vector3.zero;
        StartCoroutine(EndTelephone());
    }

    IEnumerator RingPhoneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Ring ring! The phone is ringing...");
        audioSource.Play();
        phoneRinging = true;
    }

    IEnumerator EndTelephone()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Enabling specified objects...");
        rb.velocity = originalVelocity;
        foreach (MonoBehaviour script in enabledScripts)
        {
            script.enabled = true;
        }
        foreach (GameObject obj in enabledObjects)
        {
            obj.SetActive(true);
        }
        
    }
}
