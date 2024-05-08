using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Eating : MonoBehaviour
{
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    public Camera mainCamera;
    private Collider _collider;
    private bool _Eshowed = false;

    public List<AudioClip> eatingSounds;
    private AudioSource audioSource;

    private List<GameObject> objects; // 存储盘子里的水果

    private bool _interacted = false;

    void Start()
    {
        _collider = GetComponent<Collider>();

        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;

        audioSource = GetComponent<AudioSource>();

        // 初始化水果列表
        objects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            objects.Add(child.gameObject);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (!_interacted)
        {
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
                        EatFruit();
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
        }
    }

    void EatFruit()
    {
        // 如果还有水果
        if (objects.Count > 0)
        {
            // 随机选择一个水果
            int randomIndex = Random.Range(0, objects.Count);
            GameObject fruitToEat = objects[randomIndex];

            // 禁用选中的水果
            fruitToEat.SetActive(false);
            objects.RemoveAt(randomIndex);

            // 播放随机的吃东西声音
            if (eatingSounds.Count > 0 && audioSource != null)
            {
                int randomSoundIndex = Random.Range(0, eatingSounds.Count);
                audioSource.clip = eatingSounds[randomSoundIndex];
                audioSource.Play();
            }

            if (objects.Count == 0)
            {
                _interacted = true;
                interactionIcon.SetActive(false);
            }
        }
    }
}