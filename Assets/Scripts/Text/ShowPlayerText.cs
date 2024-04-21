using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ShowPlayerText : MonoBehaviour
{
    [CanBeNull] public GameObject playerText;

// Start is called before the first frame update
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    private bool _Eshowed = false;
    private bool _hasInteracted = false;

    private Collider _collider;

  

    void Start()
    {
        _collider = GetComponent<Collider>();
        playerText.SetActive(false);
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
            if (hit.collider == _collider && !_hasInteracted)
            {
                if (!_Eshowed)
                {
                    interactionIcon.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerText.SetActive(true);
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
    }
}