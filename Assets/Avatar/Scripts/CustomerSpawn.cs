using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private int timeToSpawn;
    private ObjectPoolComp pool;
    private float time;
    [SerializeField] private GameObject[] chairs;
    [SerializeField] private Vector3 chairOffset = new Vector3(0, 1f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = GetComponent<ObjectPoolComp>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (time < timeToSpawn)
            time += Time.deltaTime;
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
}
