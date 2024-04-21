using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbledPaper : MonoBehaviour
{
    //public GameObject playerText;
    public GameObject poster;

    public GameObject wastePaper;

    //public GameObject text;
    private bool becomePoster = false;

    private bool _Eshowed = false;

    private AudioSource audioSource;


    void Start()
    {
        audioSource = poster.GetComponent<AudioSource>();
        poster.SetActive(false);
    }


    public void GetCrumbledPaper()
    {
        wastePaper.SetActive(false);
        //text.SetActive(false);
        poster.SetActive(true);

        audioSource.enabled = true;
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        becomePoster = true;
    }
}