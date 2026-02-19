using UnityEngine;
using UnityEngine.InputSystem;

public class Avatar_Move : MonoBehaviour
{
    [SerializeField] private GameObject objectInteractPoint;
    [SerializeField] private GameObject rayPoint;

    [SerializeField] private float interactRange = 0.5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform body;


    private float moveSpeed = 7f;
    private Vector2 moveInput;
    private Vector3 currentVelocity;
    private GameObject heldObject;

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        currentVelocity = move * moveSpeed;

        if (move.sqrMagnitude > 0.0001f)
        {
            AvatarTurn(move);
        }

        transform.position += currentVelocity * Time.deltaTime;
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
        Vector3 pickupCenter = objectInteractPoint.transform.position;

        Collider[] hits = Physics.OverlapSphere(pickupCenter, interactRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Food") || hit.CompareTag("Ingredient"))
            {
                heldObject = hit.gameObject;

                FoodItem food = heldObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.SetHeld(true);
                }

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }

                heldObject.transform.position = pickupCenter;
                heldObject.transform.SetParent(objectInteractPoint.transform);

                return;
            }
        }
    }

    void Drop()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        heldObject.transform.SetParent(null);
        FoodItem food = heldObject.GetComponent<FoodItem>();
        if (food != null)
        {
            food.SetHeld(false);
        }

        heldObject = null;
    }

    void AvatarTurn(Vector3 move)
    {
        if (move == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(move.normalized, Vector3.up);

        body.rotation = Quaternion.Slerp(
            body.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    void OnThrow(InputValue value)
    {
        if (!value.isPressed || heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        heldObject.transform.SetParent(null);

        Vector3 throwDir = body.forward + Vector3.up * 0.8f;
        Vector3 finalForce = throwDir.normalized * 16f + currentVelocity * 1.2f;

        rb.AddForce(finalForce, ForceMode.Impulse);
        FoodItem food = heldObject.GetComponent<FoodItem>();
        if (food != null)
        {
            food.SetHeld(false);
        }

        heldObject = null;
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed) return;

        TryInteract();
    }
    void TryInteract()
    {
        RaycastHit hit;
        Vector3 origin = rayPoint.transform.position;
        Vector3 direction = rayPoint.transform.forward; 

        Debug.DrawRay(origin, direction * 2f, Color.green, 1f); 

        if (!Physics.Raycast(origin, direction, out hit, 2f))
        {
            Debug.Log("Raycast hit nothing");
            return;
        }

        Debug.Log("Raycast hit: " + hit.collider.name);

        ChoppingSurface chopping = hit.collider.GetComponentInParent<ChoppingSurface>();
        if (chopping != null)
        {
            Debug.Log("Interacting with chopping surface");
            chopping.Interact();
            return;
        }

        FoodSpawnerSurface spawner = hit.collider.GetComponentInParent<FoodSpawnerSurface>();
        if (spawner != null && heldObject == null)
        {
            Debug.Log("Interacting with food spawner");
            spawner.Interact();
            return;
        }

        Debug.Log("Hit object has no interactable component");
    }


    public GameObject GetHeldObject()
    {
        return heldObject;
    }

    public void ClearHeldObject()
    {
        heldObject = null;
    }



}