using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleeping : MonoBehaviour
{
    public GameObject bedObject; // 床对象
    public GameObject player; // 角色对象
    public Transform wakeUpPosition; // 角色醒来的位置
    public GameObject blackScreen; // 黑屏效果的对象
    public float fadeDuration = 1.0f; // 淡入淡出的持续时间
    public GameObject day1Objects; // 第一天的对象（禁用）
    public GameObject day2Objects; // 第二天的对象（启用）

    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    private bool isSleeping = false;
    private bool _Eshowed = false;
    private bool _canMove = true;
    private Collider _collider;

    void Start()
    {
        _collider = bedObject.GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        blackScreen.SetActive(false); // 初始隐藏黑屏对象
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

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

                if (Input.GetKeyDown(KeyCode.E) && !_canMove)
                {
                    StartCoroutine(SleepAndWakeUp());
                }
            }
            else
            {
                // 如果射线击中的不是床对象，隐藏交互图标
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

    IEnumerator SleepAndWakeUp()
    {
        blackScreen.SetActive(true);

        // 淡入黑屏
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            Image image = blackScreen.GetComponent<Image>();
            image.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // 禁止移动
        _canMove = false;
        player.GetComponent<PlayerController>().enabled = false;

        // 禁用第一天的对象，启用第二天的对象
        day1Objects.SetActive(false);
        day2Objects.SetActive(true);

        // 角色位置移动到醒来的位置
        player.transform.position = wakeUpPosition.position;

        // 等待一段时间（模拟睡觉）
        yield return new WaitForSeconds(2f);

        // 淡出黑屏
        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            Image image = blackScreen.GetComponent<Image>();
            image.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        blackScreen.SetActive(false);
        _canMove = true;
        player.GetComponent<PlayerController>().enabled = true;
        isSleeping = false;
    }
}
