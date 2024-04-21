using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class enabledisableaaaaaaa : MonoBehaviour
{
    [CanBeNull] public GameObject playerText;
    public GameObject player;
    public GameObject[] disabledObjects; // 存储需要禁用的物体
    public GameObject[] enabledObjects; 
    public MonoBehaviour[] disabledScripts;
    public MonoBehaviour[] enabledScripts;

    public Camera mainCamera;
    public float interactionDistance = 2f;
    private Collider _collider;
    public GameObject interactionIcon;
    private bool _hasInteracted = false;
    private bool _Eshowed = false;


    void Start()
    {
        _collider = GetComponent<Collider>();
        mainCamera = GameManager.Camera;
        playerText.SetActive(false);
        interactionIcon.SetActive(false);
        foreach (MonoBehaviour script in disabledScripts)
        {
            script.enabled = false;
        }
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider == _collider && !_hasInteracted)
            {
                if (!_Eshowed)
                {
                    interactionIcon.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionIcon.SetActive(false);
                    playerText.SetActive(true);
                    _Eshowed = true;
                    _hasInteracted = true;
                    End();
                }
            }
            else
            {
                // 如果射线没有击中任何物体，隐藏交互图标
                interactionIcon.SetActive(false);
                _Eshowed = false;
            }
        }

    void End()
    {
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
}
