using UnityEngine;

public class FoodSpawnerSurface : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    private ItemSurface surface;

    private void Awake()
    {
        surface = GetComponent<ItemSurface>();
    }

    public void Interact()
    {
        if (surface.HasItem()) return;

        Debug.Log("Spawning food item");
        GameObject item = Instantiate(foodPrefab);
        surface.SnapItem(item);
    }
}
