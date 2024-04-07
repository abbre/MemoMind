using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public List<GameObject> foodObjects = new List<GameObject>(); // 食物对象的列表
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    private bool _Eshowed = false;

    void Start()
    {
        interactionIcon.SetActive(false); // 初始隐藏交互图标
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // 如果射线击中了可以交互的物体
            if (foodObjects.Contains(hit.collider.gameObject))
            {
                if (!_Eshowed)
                {
                    interactionIcon.SetActive(true);
                    _Eshowed = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    EatRandomFood();
                }
            }
            else
            {
                // 如果射线击中的不是食物对象，隐藏交互图标
                HideInteractionIcon();
            }
        }
        else
        {
            // 如果射线没有击中任何物体，隐藏交互图标
            HideInteractionIcon();
        }
    }

    void HideInteractionIcon()
    {
        interactionIcon.SetActive(false);
        _Eshowed = false;
    }

    void EatRandomFood()
    {
        if (foodObjects.Count > 0)
        {
            // 随机选择一个食物对象
            int randomIndex = Random.Range(0, foodObjects.Count);
            GameObject food = foodObjects[randomIndex];

            // 禁用选中的食物对象
            food.SetActive(false);

            // 从食物列表中移除被禁用的食物对象
            foodObjects.RemoveAt(randomIndex);

            // 检查食物列表是否为空，如果为空，隐藏交互图标
            if (foodObjects.Count == 0)
            {
                HideInteractionIcon();
            }
        }
    }
}
