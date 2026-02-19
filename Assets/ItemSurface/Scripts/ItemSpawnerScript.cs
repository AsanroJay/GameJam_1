using UnityEngine;

public class FoodSpawnerSurface : MonoBehaviour
{
    private ObjectPoolComp pool; // reference to the pool

    private ItemSurface surface;

    private void Awake()
    {
        surface = GetComponent<ItemSurface>();
    }

    private void Start()
    {
        pool = GetComponent<ObjectPoolComp>();
    }

    public void Interact()
    {
        if (surface.HasItem()) return;

        Debug.Log("Spawning food item from pool");

        // Get an object from the pool
        GameObject item = pool.getObject(); // uses default getObject method
        if (item == null)
        {
            Debug.LogWarning("No available objects in pool!");
            return;
        }

        item.SetActive(true);

        // Snap it to the surface
        surface.SnapItem(item);
    }
}
