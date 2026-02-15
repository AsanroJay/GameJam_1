using UnityEngine;
using UnityEngine.InputSystem;

public class Avatar_Move : MonoBehaviour
{

    private float moveSpeed = 7f;
     private Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

}
