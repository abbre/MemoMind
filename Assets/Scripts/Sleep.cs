
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public Camera mainCamera;  // 主摄像机
    public Camera sleepCamera;  // 第二摄像机
    public float transitionSpeed = 1.0f;  // 过渡速度
    public float transitionThreshold = 0.05f;  // 过渡阈值

    private Transform _mainCameraPos;
    private bool isSwitching = false;
    private bool _ePressed = false;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private void OnEnable()
    {
        sleepCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_ePressed)
        {
            _ePressed = true;
            isSwitching = true;
            mainCamera.enabled = false;
            sleepCamera.enabled = true;
            _mainCameraPos = mainCamera.transform;
            _targetPosition = _mainCameraPos.position;
            _targetRotation = _mainCameraPos.rotation;
        }

        if (isSwitching)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // 平滑过渡效果
        sleepCamera.transform.position = Vector3.Lerp(sleepCamera.transform.position, _targetPosition, transitionSpeed * Time.deltaTime);
        sleepCamera.transform.rotation = Quaternion.Lerp(sleepCamera.transform.rotation, _targetRotation, transitionSpeed * Time.deltaTime);

        // 检查是否接近目标位置和旋转
        if (Vector3.Distance(sleepCamera.transform.position, _targetPosition) < transitionThreshold &&
            Quaternion.Angle(sleepCamera.transform.rotation, _targetRotation) < transitionThreshold)
        {
            // 停止过渡
            isSwitching = false;
        }
    }
}