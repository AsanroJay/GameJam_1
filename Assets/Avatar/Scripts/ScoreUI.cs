using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI scoreText;
    private int target = 4;
    void Start()
    {
        if (scoreText == null)
            Debug.LogError("ScoreUI: scoreText NOT FOUND");
        else
            Debug.Log("ScoreUI: scoreText found → " + scoreText.name);

        Debug.Log("ScoreUI: Subscribing to SCORE_CHANGED");

        target = FindFirstObjectByType<ScoreManager>().targetScore;

        scoreText.text = "SERVED: " + 0 +"/" + target;

        EventBroadcaster.Instance.AddObserver(
            ScoreManager.Events.SCORE_CHANGED,
            UpdateScore
        );
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(ScoreManager.Events.SCORE_CHANGED);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateScore(Parameters parameters)
    {
        Debug.Log("ScoreUI: SCORE_CHANGED received");

        int newScore = parameters.GetIntExtra("score", -999);
        Debug.Log("ScoreUI: score payload = " + newScore);
        scoreText.text = "SERVED: " + newScore + "/" + target;
    }
}
