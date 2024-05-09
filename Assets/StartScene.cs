using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    void OnEnable()
    {
        firstPersonController.playerCanMove = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        firstPersonController.playerCanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
