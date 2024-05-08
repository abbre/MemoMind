using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReminderSubtitle : MonoBehaviour
{
    public InteractionTrigger interactionTrigger;
    public Subtitle subtitle;
    private bool _inCoroutine = false;
    private bool flag;

    private float _timer;
    [SerializeField] private float reminderTimeInterval;

    private bool _firstReminderPlayed = false;


    // Update is called once per frame
    void Update()
    {
        if (interactionTrigger.readyToTrigger && !interactionTrigger.hasInteracted)
        {
            _timer += Time.deltaTime;

            if (_timer < reminderTimeInterval) //如果还没到播放时间
            {
                if (subtitle.allClipsPlayed)
                    subtitle.currentClipIndex = 0;
                return;
            }

            if (_timer >= reminderTimeInterval) //播放
            {
                PlayReminderSubtitle();
                return;
            }
        }
    }


    private void PlayReminderSubtitle()
    {
        _timer = 0f;
        subtitle.PlayNextClip();
    }
}