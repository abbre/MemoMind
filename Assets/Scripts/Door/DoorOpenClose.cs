using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    public GameObject door;
    public float openAngle = 90f; // 门打开的角度
    public float closeAngle = 0f; // 门关闭的角度
    public float smooth = 2f; // 门旋转的平滑度
    public AudioClip openSound; // 开门声音
    public AudioClip closeSound; // 关门声音

    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    private bool _Eshowed = false;
    private bool _isOpen = false;
    private Quaternion _targetRotation;
    private AudioSource _audioSource;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // 如果射线击中了可以交互的物体
            if (hit.collider == _collider)
            {
                if (!_Eshowed)
                {
                    interactionIcon.SetActive(true);
                    _Eshowed = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!_isOpen)
                    {
                        // 门打开
                        _targetRotation = Quaternion.Euler(0f, openAngle, 0f);
                        PlaySound(openSound);
                        _isOpen = true;
                    }
                    else
                    {
                        // 门关闭
                        _targetRotation = Quaternion.Euler(0f, closeAngle, 0f);
                        PlaySound(closeSound);
                        _isOpen = false;
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

        if (_isOpen)
        {
            // 门旋转的平滑处理
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, _targetRotation, smooth * Time.deltaTime);

            // 判断门是否已经到达目标角度
            if (Mathf.Approximately(Quaternion.Angle(door.transform.rotation, _targetRotation), 0f))
            {
                door.transform.rotation = _targetRotation; // 确保门完全到达目标角度
                _isOpen = false;
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
