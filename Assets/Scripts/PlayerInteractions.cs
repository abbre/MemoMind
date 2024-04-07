using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractions : MonoBehaviour
{

    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;
    private AudioSource audioSource;
    private float stepTimer = 0f;

    public GameObject player;
    public Transform wakeUpPosition;
    public float fadeDuration = 1.0f;
    public Image blackScreen;

    private bool isSleeping = false;

    void Start()
    {
        blackScreen.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsMoving() && stepTimer <= 0f)
        {
            PlayRandomFootstepSound();
            stepTimer = stepInterval;
        }

        stepTimer -= Time.deltaTime;
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

    private bool IsMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
    }

    private void PlayRandomFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        int randomIndex = Random.Range(0, footstepSounds.Length);
        audioSource.clip = footstepSounds[randomIndex];
        audioSource.Play();
    }
}