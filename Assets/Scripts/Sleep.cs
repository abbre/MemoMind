using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
  
// Start is called before the first frame update
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    public Camera SleepCamera;
    private bool _Eshowed = false;

    private Collider _collider;

    public Image blackScreen;

    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;
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
                    mainCamera.enabled = false;
                    SleepCamera.enabled = true;
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

        if (SleepCamera.enabled)
        {
            // 逐渐增加遮罩的透明度
            Color currentColor = blackScreen.color;
            currentColor.a += Time.deltaTime * 0.5f; 
            blackScreen.color = currentColor;
        }

    }
}
