using UnityEngine;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> objectsToGenerate; // 要生成的物体列表
    public int maxObjects = 500; // 最大允许的物体数量
    public float generationInterval = 1f; // 生成物体的时间间隔
    public float speed = 5f; // 物体飞出的速度

    private Queue<GameObject> generatedObjects = new Queue<GameObject>(); // 存储已生成的物体
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        // 当计时器超过生成间隔时，生成新物体
        if (timer >= generationInterval)
        {
            GenerateObject();
            timer = 0f;
        }

        // 如果生成的物体数量超过最大限制，则移除最早生成的物体
        while (generatedObjects.Count > maxObjects)
        {
            GameObject objToRemove = generatedObjects.Dequeue();
            Destroy(objToRemove);
        }
    }

    // 生成新物体并添加到队列中
    private void GenerateObject()
    {
        if (objectsToGenerate.Count == 0)
        {
            Debug.LogError("No objects to generate!");
            return;
        }

        GameObject selectedObject = objectsToGenerate[Random.Range(0, objectsToGenerate.Count)]; // 从列表中随机选择一个物体预制体

        Vector3 randomDirection = Random.insideUnitSphere.normalized; // 随机方向

        GameObject newObject = Instantiate(selectedObject, transform.position, Quaternion.identity);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = randomDirection * speed; // 设置物体的速度方向
        }
        generatedObjects.Enqueue(newObject);
    }
}