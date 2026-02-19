using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        if (timerText == null)
            Debug.LogError("TimerUI: timerText NOT FOUND");
        else
            Debug.Log("TimerUI: timerText found → " + timerText.name);

        EventBroadcaster.Instance.AddObserver(
            TimerManager.Events.TIMER_UPDATED,
            UpdateTimer
        );

        timerText.text = "Time: 0";
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(TimerManager.Events.TIMER_UPDATED);
    }

    public void UpdateTimer(Parameters parameters)
    {
        float timeLeft = parameters.GetFloatExtra("time", -1f);
        if (timeLeft < 0f)
        {
            Debug.LogWarning("TimerUI: invalid time value received");
            return;
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}