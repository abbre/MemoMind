using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Transform handTransform; // 玩家手的位置
    public float pickupRange = 2.5f; // 拾取范围

    private bool isHeld = false;
    private float lastPickupTime = 0f; // 记录上一次拾取的时间

    void Update()
    {
        // 发射射线检测玩家能否拾取物体
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 从屏幕中心发出射线

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // 如果射线击中了可以拾取的物体
            if (hit.collider.gameObject == gameObject && Input.GetKeyDown(KeyCode.E))
            {
                if (!isHeld)
                {
                    // 将物品移动到玩家手的位置
                    transform.SetParent(handTransform);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    transform.localScale = Vector3.one;

                    // 将物体的刚体组件禁用，防止物品在手上时受到物理影响
                    Rigidbody rb = GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    isHeld = true;
                }
                else
                {
                    // 检查上一次拾取的时间，如果在一定时间间隔内再次按下 "E" 键，则放下物品
                    float timeSinceLastPickup = Time.time - lastPickupTime;
                    if (timeSinceLastPickup >= 0.5f) // 设置一个时间间隔，例如 0.5 秒
                    {
                        // 将物品移回原始位置
                        transform.SetParent(null);

                        // 恢复物体的刚体组件
                        Rigidbody rb = GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                        }

                        isHeld = false;
                    }
                }

                lastPickupTime = Time.time; // 更新上一次拾取的时间
            }
        }
    }
}
