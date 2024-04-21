using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{

    private Collider _collider;
    private bool _Eshowed = false;

    public List<AudioClip> eatingSounds;
    private AudioSource audioSource;

    private List<GameObject> objects; // 存储盘子里的水果

    void Start()
    {
        _collider = GetComponent<Collider>();

        //interactionIcon.SetActive(false); // 初始隐藏交互图标
    
        audioSource = GetComponent<AudioSource>();

        // 初始化水果列表
        objects = new List<GameObject>();
        foreach (Transform child in transform)
        {
           objects.Add(child.gameObject);
        }
    }
    

    public void EatDrink()
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
        }
        
      
    }
}
