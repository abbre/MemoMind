using System.Collections.Generic;
using UnityEngine;

public class DisableTexts : MonoBehaviour
{
    public string tagToFind = "PlayerText"; // 标签名称
    private List<GameObject> taggedObjects = new List<GameObject>(); // 存储具有相同标签的游戏对象的列表

    void Start()
    {
        // 获取所有具有指定标签的游戏对象
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToFind);
        
        // 将找到的游戏对象添加到列表中
        taggedObjects.AddRange(objectsWithTag);

        // 禁用所有找到的游戏对象
        SetObjectsActive(false);
    }

    // 设置所有游戏对象的状态（激活/禁用）
    private void SetObjectsActive(bool isActive)
    {
        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(isActive);
        }
    }
}