using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f; // 鼠标灵敏度

    float xRotation = 0f; // 用于存储摄像机在X轴上的旋转角度

    void Start()
    {
        // 锁定鼠标光标到屏幕中心，使其不可见，并且限制在游戏窗口中
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 获取鼠标在X和Y轴上的移动距离
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // 根据鼠标的移动距离来更新摄像机的旋转角度
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 限制摄像机在上下方向上的旋转角度，避免翻转

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 在X轴上旋转摄像机
        transform.parent.Rotate(Vector3.up * mouseX); // 在Y轴上旋转摄像机的父对象（通常是玩家的角色对象）
    }
}