using UnityEngine;
using UnityEngine.InputSystem;

public class Avatar_Move : MonoBehaviour
{
    [SerializeField] private GameObject objectInteractPoint;
    [SerializeField] private float interactRange = 2f;

    private float moveSpeed = 7f;
    private Vector2 moveInput;

    private GameObject heldObject;

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnPickUp(InputValue value)
    {
        if (!value.isPressed) return;

        if (heldObject == null)
        {
            TryPickUp();
        }
        else
        {
            Drop();
        }
    }

    void TryPickUp()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);

    

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Food"))
            {
                heldObject = hit.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                heldObject.transform.position = objectInteractPoint.transform.position;
                heldObject.transform.SetParent(objectInteractPoint.transform);
                return;
            }
        }
    }

    void Drop()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        heldObject.transform.SetParent(null);
        heldObject = null;
    }
}
