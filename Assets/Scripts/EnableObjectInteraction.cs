
using UnityEngine;

public class EnableObjectInteraction<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T InteractionScript;
    
    // Start is called before the first frame update
    void Start()
    {
        InteractionScript = GetComponent<T>();
        InteractionScript.enabled = false;
    }

    // 启用对象的交互
    public void EnableInteraction()
    {
        InteractionScript.enabled = true;
    }
}