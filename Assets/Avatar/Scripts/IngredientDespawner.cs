using UnityEngine;

public class DestroyerComp : MonoBehaviour
{
    [SerializeField] private bool toDestroy;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            if (toDestroy)
                Destroy(collision.gameObject);
            else
                collision.gameObject.SetActive(false);
        }
    }
}
