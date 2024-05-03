using System.Text;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

public class TransferPos : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private float transferTime;

    public UnityEvent TriggerNextObject;

    public void MoveToTargetPos()
    {
        transform.DOMove(endValue: targetPos.position, transferTime);
        transform.DORotateQuaternion(targetPos.rotation, transferTime);
        //transform.DOMove(targetPos.localScale, transferTime);
        TriggerNextObject?.Invoke();
    }
}