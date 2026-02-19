using UnityEngine;
using UnityEngine.InputSystem;

public class Avatar_Move : MonoBehaviour
{
    [SerializeField] private GameObject objectInteractPoint;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform body;


    private float moveSpeed = 7f;
    private Vector2 moveInput;

    private GameObject heldObject;

    private void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        if (move.sqrMagnitude > 0.0001f)
        {
            avatarTurn(move);
        }

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (heldObject != null && heldObject.CompareTag("Untagged"))
        {
            heldObject = null;
        }
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
            if (hit.CompareTag("Food") || hit.CompareTag("Ingredient"))
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

    void avatarTurn(Vector3 move)
    {
        if (move == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(move.normalized, Vector3.up);

        body.rotation = Quaternion.Slerp(
            body.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

}
