using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60f; // in seconds
    private float currentTime;
    private Boolean timerRunning = false;

    public class Events
    {
        public const string TIMER_UPDATED = "TIMER_UPDATED";
        public const string TIMER_ENDED = "TIMER_ENDED";
    }

    void Start()
    {
        currentTime = startTime;
        timerRunning = true;

        EventBroadcaster.Instance.AddObserver(ScoreManager.Events.GAME_WON, StopTimer);
    }

    void Update()
    {
        if (timerRunning) { 
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0f) currentTime = 0f;

                Parameters param = new Parameters();
                param.PutExtra("time", currentTime);
                EventBroadcaster.Instance.PostEvent(Events.TIMER_UPDATED, param);

                if (currentTime <= 0f)
                {
                    timerRunning = false;
                    Debug.Log("TimerManager: TIMER_ENDED broadcasting");
                    EventBroadcaster.Instance.PostEvent(Events.TIMER_ENDED, null);
                }
            }
        }
    }

    public void StopTimer(Parameters parameters)
    {
        timerRunning = false;
    }

    public void ResetTimer(float newTime)
    {
        currentTime = newTime;
    }
}