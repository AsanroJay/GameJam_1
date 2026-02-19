using UnityEngine;

public class IgnoreFoodCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Collider myCollider = GetComponent<Collider>();
            Collider foodCollider = collision.collider;

            Physics.IgnoreCollision(myCollider, foodCollider);
        }
    }
}
