using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MoveToPicture : MonoBehaviour
{
    [SerializeField] private Transform familyPicPos;
    [SerializeField] private float transformTime = 1.0f;
    public FirstPersonController firstPersonController;

    [SerializeField] private float timeBeforeBlackScreenFadeIn;
    [SerializeField] private float timeBeforeSwitchScene;
    public UnityEvent LoadScene;
    public UnityEvent BlackSceneFadeIn;

    // Start is called before the first frame update
    public void moveToFamilyPicture()
    {   
        transform.DOMove(familyPicPos.position, transformTime);
        firstPersonController.cameraCanMove = false;
        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(timeBeforeBlackScreenFadeIn);
        BlackSceneFadeIn.Invoke();
        yield return new WaitForSeconds(timeBeforeSwitchScene);
        LoadScene.Invoke();
    }
}