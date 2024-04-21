
using UnityEngine;

public class ShowPlayerTextWithoutInteraction : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowPlayerTextAuto()
    {
       gameObject.SetActive(true);
    }
}
