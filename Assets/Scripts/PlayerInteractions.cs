using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject player;
    public Transform wakeUpPosition;
    public float fadeDuration = 1.0f;
    public Image blackScreen;

    private bool isSleeping = false;

    void Start()
    {
        blackScreen.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!isSleeping && gameObject.CompareTag("Bed"))
        {
            isSleeping = true;
            StartCoroutine(SleepAndWakeUp());
        }
    }

    IEnumerator SleepAndWakeUp()
    {
        blackScreen.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Character goes to sleep
        player.SetActive(false);

        yield return new WaitForSeconds(2f); // Sleep for 2 seconds

        // Character wakes up
        player.transform.position = wakeUpPosition.position;

        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
        isSleeping = false;
        player.SetActive(true);
    }
}