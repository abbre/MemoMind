using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HoldAndThrow : MonoBehaviour
{
    [SerializeField] private Transform holdTransform;

    [SerializeField] private Transform throwTransform;

    public FirstPersonController firstPersonController;
    private Rigidbody _wastePaperRb;

    public UnityEvent EnableNext;
    
    void OnEnable()
    {
        _wastePaperRb = gameObject.GetComponent<Rigidbody>();
    }

    public void StartHoldThrow()
    {
        StartCoroutine(HoldThenThrow());
        firstPersonController.cameraCanMove = false;
        firstPersonController.playerCanMove = false;
    }

    private IEnumerator HoldThenThrow()
    {
        MoveToHoldTransform();
        yield return new WaitForSeconds(2f);
        MoveToThrowTransform();
        firstPersonController.cameraCanMove = true;
        firstPersonController.playerCanMove = true;
        EnableNext.Invoke();
    }

    private void MoveToHoldTransform()
    {
        gameObject.transform.position = holdTransform.position;
        gameObject.transform.rotation = holdTransform.rotation;
    }

    private void MoveToThrowTransform()
    {
        gameObject.transform.position = throwTransform.position;
        gameObject.transform.rotation = throwTransform.rotation;
        _wastePaperRb.isKinematic = false;
    }
}