using UnityEngine;

public class VisualTags : MonoBehaviour
{
    public Transform player;
    public float labelDistance = 5f;
    public GUIStyle labelStyle; // 用于自定义标签的样式

    private void OnGUI()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Interactable");

        foreach (GameObject obj in objects)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            float distance = Vector3.Distance(obj.transform.position, player.position);

            if (screenPos.z > 0 && distance <= labelDistance)
            {
                // 如果物体在主角视野内且距离小于等于labelDistance
                GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 200, 50), obj.name, labelStyle);
            }
        }
    }
}
