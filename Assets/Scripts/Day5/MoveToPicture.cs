using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToPicture : MonoBehaviour
{
    [SerializeField] private Transform familyPicPos;
    public FirstPersonController firstPersonController;

    // Start is called before the first frame update
    public void moveToFamilyPicture()
    {
        transform.DOMove(familyPicPos.position,1f);
        firstPersonController.cameraCanMove = false;
    }
}
