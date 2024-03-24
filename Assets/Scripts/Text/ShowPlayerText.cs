using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPlayerText : MonoBehaviour
{
    public GameObject playerText;

// Start is called before the first frame update
    void Start()
    {
        playerText.SetActive(false);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        playerText.SetActive(true);
    }
}