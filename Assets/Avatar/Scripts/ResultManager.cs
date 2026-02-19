using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI remainingTimeText;

    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private GameObject exitButton;

    private float latestTime = 0f;

    void Start()
    {
        if (resultPanel == null || messageText == null || remainingTimeText == null)
        {
            Debug.LogError("ResultManager: Missing UI references!");
            return;
        }

        resultPanel.SetActive(false);

        EventBroadcaster.Instance.AddObserver(TimerManager.Events.TIMER_UPDATED, OnTimerUpdated);
        EventBroadcaster.Instance.AddObserver(ScoreManager.Events.GAME_WON, OnGameWon);
        EventBroadcaster.Instance.AddObserver(ScoreManager.Events.GAME_LOST, OnGameLost);

        playAgainButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnPlayAgainClicked);
        exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnExitClicked);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(TimerManager.Events.TIMER_UPDATED);
        EventBroadcaster.Instance.RemoveObserver(ScoreManager.Events.GAME_WON);
        EventBroadcaster.Instance.RemoveObserver(ScoreManager.Events.GAME_LOST);
    }

    private void OnGameWon(Parameters parameters)
    {
        ShowResult("YOU WIN!", parameters);
    }

    private void OnGameLost(Parameters parameters)
    {
        ShowResult("YOU LOSE!", parameters);
    }

    private void OnTimerUpdated(Parameters param)
    {
        latestTime = param.GetFloatExtra("time", 0f);
    }

    private void ShowResult(string message, Parameters parameters)
    {
        messageText.text = message;

        float remainingTime = latestTime;

        Time.timeScale = 0f;

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        remainingTimeText.text = $"Time Remaining {minutes:00}:{seconds:00}";

        resultPanel.SetActive(true);
    }

    private void OnPlayAgainClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        resultPanel.SetActive(false);
    }

    private void OnExitClicked()
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); // quit application
        #endif
    }
}