using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] public int targetScore = 1;

    public class Events
    {
        public const string ON_PRIMARY_INTERACTION = "ON_PRIMARY_INTERACTION";
        public const string SCORE_CHANGED = "SCORE_CHANGED";
        public const string GAME_WON = "GAME_WON";
        public const string GAME_LOST = "GAME_LOST";
    }

    void Start()
    {
        EventBroadcaster.Instance.AddObserver(Customer_Action.Events.FOOD_TAKEN, OnFoodTaken);

        EventBroadcaster.Instance.AddObserver(TimerManager.Events.TIMER_ENDED, OnTimerEnded);

        Debug.Log("ScoreManager: Start() running, subscribing to FOOD_TAKEN and TIMER_ENDED");
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(Customer_Action.Events.FOOD_TAKEN);
        EventBroadcaster.Instance.RemoveObserver(TimerManager.Events.TIMER_ENDED);
        Debug.Log("ScoreManager: OnDestroy() running, unsubscribing from FOOD_TAKEN and TIMER_ENDED");
    }

    void OnFoodTaken(Parameters parameters)
    {
        Debug.Log("ScoreManager: FOOD_TAKEN received");
        score += 1;

        Parameters p = new Parameters();
        p.PutExtra("score", score);

        EventBroadcaster.Instance.PostEvent(Events.SCORE_CHANGED, p);
        Debug.Log("ScoreManager: Broadcasting SCORE_CHANGED with score = " + score);

        Debug.Log("ScoreManager: Checking if score("+score+") >= targetScore (" + targetScore + ")");
        if (score >= targetScore)
        {
            Debug.Log("ScoreManager: Target reached! Broadcasting GAME_WON");
            EventBroadcaster.Instance.PostEvent(Events.GAME_WON, null);
        }
    }

    void OnTimerEnded(Parameters parameters)
    {
        Debug.Log("ScoreManager: TIMER_ENDED received");

        if (score < targetScore)
        {
            Debug.Log("ScoreManager: Timer ended and target not reached. Broadcasting GAME_LOST");
            EventBroadcaster.Instance.PostEvent(Events.GAME_LOST, null);
        }
        else
        {
            Debug.Log("ScoreManager: Timer ended but target already reached, GAME_WON already broadcast");
        }
    }
}