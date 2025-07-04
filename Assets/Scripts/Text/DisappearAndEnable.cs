using System.Collections;
using Febucci.UI.Core;
using JetBrains.Annotations;
using UnityEngine;

public class DisappearAndEnable : MonoBehaviour
{
    public float timeBeforeTextDisappear = 2f;
    public TypewriterCore textAnimator;
    [CanBeNull]public GameObject nextPlayerText;
    public bool hasNext = true; // 控制是否有下一个文字

    public void StartDisappearing()
    {
        StartCoroutine(StartDisappearingDelay());
    }

    private IEnumerator StartDisappearingDelay()
    {
        yield return new WaitForSeconds(timeBeforeTextDisappear);
        textAnimator.StartDisappearingText();
    }

    public void EnableNextText()
    {
        if(hasNext) // 检查是否有下一个文字
        {
            StartCoroutine(EnableNextTextDelayed());
        }
    }

    private IEnumerator EnableNextTextDelayed()
    {
        yield return new WaitForSeconds(timeBeforeTextDisappear);
        nextPlayerText.SetActive(true);
    }
}