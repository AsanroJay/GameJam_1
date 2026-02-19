using UnityEngine;

public class DestroyerComp : MonoBehaviour
{
    [SerializeField] private bool toDestroy;
    [SerializeField] private GameObject spawnerLocation;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            if (toDestroy)
                Destroy(collision.gameObject);
            else
                collision.gameObject.SetActive(false);
        }
    }
}
