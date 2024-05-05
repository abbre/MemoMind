using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitchenToilet : MonoBehaviour
{
    private Collider _collider;
    private Quaternion _targetRotation;
    public float openAngle = 90f; // 门打开的角度
    [SerializeField] private GameObject[] doors;

    public GameObject IllusiveDoors;

    [SerializeField] private GameObject RestTrigger;
    public UnityEvent EnableNext;


    void Start()
    {
        IllusiveDoors.SetActive(false);
        RestTrigger.SetActive(false);
    }

    // Update is called once per frame
    private void OpenDoor()
    {
        foreach (var eachDoor in doors)
        {
            _targetRotation = Quaternion.Euler(0f, openAngle, 0f);
            eachDoor.transform.rotation = _targetRotation;
        }
    }

    private void EnableIllusiveDoor()
    {
        IllusiveDoors.SetActive(true);
    }

    private void EnableRestTrigger()
    {
        RestTrigger.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
            EnableIllusiveDoor();
            EnableRestTrigger();
            StartCoroutine(TriggerNextText());
        }
    }

    private IEnumerator TriggerNextText()
    {
        yield return new WaitForSeconds(3f);
        EnableNext.Invoke();
    }
}