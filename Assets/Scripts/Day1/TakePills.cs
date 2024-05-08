using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePills : MonoBehaviour
{
    public Transform eatPillTransform;
    public FirstPersonController firstPersonController;

    private GameObject step;
    private AudioSource stepAudio;

    private bool _audioIsPlayed = false;

    [SerializeField] private AudioSource swallowAudio;

    public float holdTime = 1.5f;
    // Start is called before the first frame update

    private void Start()
    {
        step = GameObject.Find("Step");
        stepAudio = step.GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log(firstPersonController.playerCanMove);
    }
    public void HoldPill()
    {
        firstPersonController.playerCanMove = false;
        firstPersonController.cameraCanMove = false;
        transform.position = eatPillTransform.position;
        transform.rotation = eatPillTransform.rotation;
        transform.localScale = eatPillTransform.localScale;
    
        StartCoroutine(PillDisappear());
        stepAudio.enabled = false;
        swallowAudio.PlayOneShot(swallowAudio.clip);
    }


    private IEnumerator PillDisappear()
    {
        yield return new WaitForSeconds(holdTime);
        gameObject.SetActive(false);
        firstPersonController.playerCanMove = true;
        firstPersonController.cameraCanMove = true;
        stepAudio.enabled = true;
    }
}