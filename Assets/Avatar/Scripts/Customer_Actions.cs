using TMPro;
using UnityEngine;

public class Customer_Action : MonoBehaviour
{
    [SerializeField] private GameObject objectInteractPoint;
    [SerializeField] private float interactRange = 1f;
    [SerializeField] private float walkSpeed = 5f;

    private GameObject heldObject;
    private Vector3 targetPosition;
    private bool hasTarget = false;
    private bool isExiting = false;
    private GameObject chair;
    public class Events
    {
        public const string ON_PRIMARY_INTERACTION = "ON_PRIMARY_INTERACTION";
        public const string FOOD_TAKEN = "FOOD_TAKEN";

        public const string X_POS = "X_POS";
        public const string Y_POS = "Y_POS";
        public const string Z_POS = "Z_POS";
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("ON_PRIMARY_INTERACTION");
        EventBroadcaster.Instance.RemoveObserver("FOOD_TAKEN");
        Debug.Log("Customer destroyed and observer removed.");
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (heldObject == null)
        {
            LookForFood();
        }
        if (targetPosition != null)
        {
            MoveToTarget();
            if(isExiting && Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                this.gameObject.SetActive(false);
                isExiting = false;

                if (heldObject != null)
                {
                    heldObject.transform.SetParent(null);
                    heldObject.SetActive(false);
                    heldObject.transform.position = Vector3.zero;
                }

            }
        }
    }

    public void SetDestination(GameObject dest, Vector3 offset)
    {
        Vector3 targetCoords = dest.transform.position + offset;
        chair = dest;
        targetPosition = targetCoords;
        hasTarget = true;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            hasTarget = false;
        }
    }

    public void LeaveRestaurant(GameObject exitPoint)
    {
        if (chair != null)
        {
            chair.tag = "Chair";
            chair = null;
        }

        targetPosition = exitPoint.transform.position;
        transform.Rotate(0, 180, 0);
        isExiting = true;
    }

    void LookForFood()
    {
        Collider[] hits = Physics.OverlapSphere(objectInteractPoint.transform.position, interactRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Food"))
            {
                GrabFood(hit.gameObject);
                break;
            }
        }
    }

    void GrabFood(GameObject food)
    {
        heldObject = food;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        heldObject.transform.position = objectInteractPoint.transform.position;
        heldObject.transform.SetParent(objectInteractPoint.transform);

        Parameters param = new Parameters();

        Debug.Log("Customer: FOOD_TAKEN event firing");
        EventBroadcaster.Instance.PostEvent(Events.FOOD_TAKEN, param);

        LeaveRestaurant(GameObject.FindGameObjectWithTag("ExitPoint"));
        return;
    }


}