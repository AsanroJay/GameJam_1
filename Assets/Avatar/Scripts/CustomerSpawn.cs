using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private int timeToSpawn;
    private ObjectPoolComp pool;
    private float time;
    [SerializeField] private GameObject[] chairs;
    [SerializeField] private Vector3 chairOffset = new Vector3(0, 1f, 0);

    private bool spawningActive = true; // control spawning

    void Start()
    {
        pool = GetComponent<ObjectPoolComp>();
        time = timeToSpawn;

        // Subscribe to game end events
        EventBroadcaster.Instance.AddObserver(ScoreManager.Events.GAME_WON, OnGameEnded);
        EventBroadcaster.Instance.AddObserver(ScoreManager.Events.GAME_LOST, OnGameEnded);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        EventBroadcaster.Instance.RemoveObserver(ScoreManager.Events.GAME_WON);
        EventBroadcaster.Instance.RemoveObserver(ScoreManager.Events.GAME_LOST);
    }
    void Update()
    {
        if (!spawningActive) return;

        if (time < timeToSpawn)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;

            GameObject obj = pool.getObject(transform.position, transform.rotation);
            if (obj == null) return;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);

            foreach (GameObject chair in chairs)
            {
                if (chair.CompareTag("Chair"))
                {
                    chair.tag = "Occupied";

                    Customer_Action action = obj.GetComponent<Customer_Action>();
                    if (action != null)
                    {
                        action.SetDestination(chair, chairOffset);
                    }
                    else
                    {
                        Debug.Log("No chairs available! Customer is loitering.");
                    }
                    break;
                }
            }
        }
    }

    private void OnGameEnded(Parameters parameters)
    {
        spawningActive = false;
    }
}