using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPlayerText2 : MonoBehaviour
{
    public GameObject playerText;
    public GameObject poster;
    public GameObject wastePaper;
    public GameObject text;
    private bool becomePaperBall = false;

    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    private Collider _collider;
    private bool _Eshowed = false;

    private AudioSource audioSource;
    public bool readyToTrigger = false;

    void Start()
    {
        _collider = GetComponent<Collider>();
        playerText.SetActive(false);
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;
        wastePaper.GetComponent<Rigidbody>().isKinematic = true;
        audioSource = wastePaper.GetComponent<AudioSource>();
    }

    public void SetReadyToTrigger()
    {
        readyToTrigger = true;
    }

    void Update()
    {
        if (readyToTrigger)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // 如果射线击中了可以交互的物体
                if (hit.collider == _collider)
                {
                    if (!_Eshowed && !becomePaperBall)
                    {
                        interactionIcon.SetActive(true);
                        _Eshowed = true;
                    }

                    if (Input.GetKeyDown(KeyCode.E) && !becomePaperBall)
                    {
                        playerText.SetActive(true);
                        poster.GetComponent<MeshRenderer>().enabled = false;
                        text.SetActive(false);
                        interactionIcon.SetActive(false);
                        
                        wastePaper.GetComponent<MeshRenderer>().enabled = true;

                        audioSource.enabled = true;
                        if (audioSource != null && !audioSource.isPlaying)
                        {
                            audioSource.Play();
                        }

                        wastePaper.GetComponent<Rigidbody>().isKinematic = false;
                        _Eshowed = true;

                        becomePaperBall = true;
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
}