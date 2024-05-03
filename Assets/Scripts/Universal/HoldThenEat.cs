using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class HoldThenEat : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    public FirstPersonController firstPersonController;

    [SerializeField] private bool _hasEatMovement;
    [SerializeField] private bool _triggerWhiteScreen;

    [SerializeField] private float timeBeforeEating;
    [SerializeField] private Transform eatPos;
    [SerializeField] private float TransferTimeForEatMovement;
    [SerializeField] private float timeBeforeScreenStartTurning;

    public UnityEvent TriggerWhiteScreen;


    public void TransferPositionSuddenly()
    {
        transform.position = targetPos.position;
        transform.rotation = targetPos.rotation;
        firstPersonController.playerCanMove = false;
        firstPersonController.cameraCanMove = false;

        if (_hasEatMovement)
        {
            StartCoroutine(EatMovement());
        }
    }

    private IEnumerator EatMovement()
    {
        yield return new WaitForSeconds(timeBeforeEating);
        transform.DOMove(eatPos.position, TransferTimeForEatMovement);
        transform.DORotateQuaternion(eatPos.rotation, TransferTimeForEatMovement);

        if (_triggerWhiteScreen)
        {
            yield return new WaitForSeconds(timeBeforeScreenStartTurning);
            TriggerWhiteScreen.Invoke();
        }
    }
}