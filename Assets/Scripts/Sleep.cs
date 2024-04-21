
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Sleep : MonoBehaviour
{
  
// Start is called before the first frame update
    public GameObject interactionIcon; // 交互图标
    public float interactionDistance = 3f; // 相机检测距离

    private bool _readyToSleep = false;

    public Camera mainCamera;
    public Camera SleepCamera;
    private bool _Eshowed = false;

    private Collider _collider;

    public Image blackScreen;
    private bool _hasInteracted = false;

    public UnityEvent loadScene;

    
    void Start()
    {
        _collider = GetComponent<Collider>();
        interactionIcon.SetActive(false); // 初始隐藏交互图标
        mainCamera = GameManager.Camera;
        blackScreen.enabled = false;
    }

    public void SetReadyToSleep()
    {
        _readyToSleep = true;
    }

    void Update()
    {
        if(_readyToSleep)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // 如果射线击中了可以交互的物体
                if (hit.collider == _collider && !_hasInteracted)
                {
                    if (!_Eshowed)
                    {
                        interactionIcon.SetActive(true);
                        
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        mainCamera.enabled = false;
                        SleepCamera.enabled = true;
                        blackScreen.enabled = true;
                        interactionIcon.SetActive(false);
                        _Eshowed = true;
                        _hasInteracted = true;;
                    }
                }
                else
                {
                    // 如果射线没有击中任何物体，隐藏交互图标
                    interactionIcon.SetActive(false);
                    _Eshowed = false;
                }
            }

            if (SleepCamera.enabled)
            {
                Color currentColor = blackScreen.color;
                currentColor.a += Time.deltaTime * 0.5f;
                blackScreen.color = currentColor;
                if (currentColor.a >= 1.0f)
                {
                    loadScene.Invoke();
                }
            }
        }
    }
}
