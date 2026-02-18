using UnityEngine;

public class Customer_Action : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject objectInteractPoint;
    [SerializeField] private float interactRange = 1f;

    private GameObject heldObject;

    void Update()
    {
        if (heldObject == null)
        {
            LookForFood();
        }
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
        heldObject.tag = "Untagged";

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        heldObject.transform.position = objectInteractPoint.transform.position;
        heldObject.transform.SetParent(objectInteractPoint.transform);

        return;
    }
}