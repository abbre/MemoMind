using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetDoorsOpen : MonoBehaviour
{
    public GameObject leftDoor;
    public float leftOpenAngle = 90f; // 左门打开的角度
    public float leftCloseAngle = 0f; // 左门关闭的角度

    public GameObject rightDoor;
    public float rightOpenAngle = -90f; // 右门打开的角度
    public float rightCloseAngle = 0f; // 右门关闭的角度

    public float smooth = 2f; // 门旋转的平滑度

    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    private bool _Eshowed = false;
    private bool _areOpen = false;
    private bool _isMoving = false;
    private Quaternion _leftTargetRotation;
    private Quaternion _rightTargetRotation;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
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
                    if (!_areOpen && !_isMoving)
                    {
                        // 同时打开左右两扇门
                        _leftTargetRotation = Quaternion.Euler(0f, leftOpenAngle, 0f);
                        _rightTargetRotation = Quaternion.Euler(0f, rightOpenAngle, 0f);
                        _areOpen = true;
                        _isMoving = true;
                    }
                    else if (_areOpen && !_isMoving)
                    {
                        // 同时关闭左右两扇门
                        _leftTargetRotation = Quaternion.Euler(0f, leftCloseAngle, 0f);
                        _rightTargetRotation = Quaternion.Euler(0f, rightCloseAngle, 0f);
                        _areOpen = false;
                        _isMoving = true;
                    }
                    interactionIcon.SetActive(false);
                    _Eshowed = false;
                }
            }
            else
            {
                // 如果射线没有击中任何物体，隐藏交互图标
                interactionIcon.SetActive(false);
                _Eshowed = false;
            }
        }

        if (_isMoving)
        {
            // 门旋转的平滑处理
            leftDoor.transform.rotation = Quaternion.Slerp(leftDoor.transform.rotation, _leftTargetRotation, smooth * Time.deltaTime);
            rightDoor.transform.rotation = Quaternion.Slerp(rightDoor.transform.rotation, _rightTargetRotation, smooth * Time.deltaTime);

            // 判断门是否已经到达目标角度
            if (Quaternion.Angle(leftDoor.transform.rotation, _leftTargetRotation) < 1f &&
                Quaternion.Angle(rightDoor.transform.rotation, _rightTargetRotation) < 1f)
            {
                _isMoving = false;
            }
        }
    }
}
