using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanPlayerMovement : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    [SerializeField] private AudioSource stepAudio;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController.playerCanMove = false;
        stepAudio.enabled = false;

    }
}
