using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class day4Whitescreen : MonoBehaviour
{
    public GameObject whiteScreenUI; // 用于覆盖整个画面的白色UI
    public float transitionSpeed = 0.5f; // 颜色变化的速度
    public float waitTime = 3f; // 等待时间
    public string nextSceneName; // 下一个场景的名称
    public GameObject activatedObject;
    private bool transitionStarted = false;
    private float targetAlpha = 0f;

    void Update()
    {
        // 如果物体的x坐标小于-8，并且过渡尚未开始，则开始过渡
        if (transform.position.x < -8 && !transitionStarted)
        {
            transitionStarted = true;
            targetAlpha = 1f; // 将目标透明度设置为1，完全不透明
            activatedObject.SetActive(true);
        }

        // 控制UI元素的透明度渐变
        if (transitionStarted && whiteScreenUI != null)
        {
            Color newColor = whiteScreenUI.GetComponent<Image>().color;
            newColor.a = Mathf.MoveTowards(newColor.a, targetAlpha, transitionSpeed * Time.deltaTime);
            whiteScreenUI.GetComponent<Image>().color = newColor;

            // 如果UI完全不透明，并且已经等待了指定的时间，则切换到下一个场景
            if (newColor.a == 1f && !string.IsNullOrEmpty(nextSceneName))
            {
                Invoke("LoadNextScene", waitTime);
            }
        }
    }

    // 切换到下一个场景
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}