using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    public class Events
    {
        public const string ON_PRIMARY_INTERACTION = "ON_PRIMARY_INTERACTION";
        public const string SCORE_CHANGED = "SCORE_CHANGED";
    }

    void Start()
    {
        EventBroadcaster.Instance.AddObserver(
            Customer_Action.Events.FOOD_TAKEN,
            OnFoodTaken
        );

        Debug.Log("ScoreManager: Start() running, subscribing to FOOD_TAKEN");
    }

    void OnFoodTaken(Parameters parameters)
    {
        Debug.Log("ScoreManager: FOOD_TAKEN received");
        score += 10; // simple example

        Parameters p = new Parameters();
        p.PutExtra("score", score);

        Debug.Log("ScoreManager: Broadcasting SCORE_CHANGED with score = " + score);

        EventBroadcaster.Instance.PostEvent(
            Events.SCORE_CHANGED,
            p
        );

        Debug.Log("Score updated: " + score);
    }
}