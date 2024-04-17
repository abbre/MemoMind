using UnityEngine;
using System.Collections;

public class DelayedActivation : MonoBehaviour
{
    public GameObject targetObject; // 要延迟激活的 GameObject
    public float delayTime = 10f; // 延迟时间

    private void Start()
    {
        // 开启协程进行延迟激活
        StartCoroutine(ActivateAfterDelay(delayTime));
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        // 激活目标 GameObject
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}