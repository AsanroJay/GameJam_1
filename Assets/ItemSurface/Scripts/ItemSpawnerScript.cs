using UnityEngine;

public class FoodSpawnerSurface : MonoBehaviour
{
    [SerializeField] private ObjectPoolComp foodPool; // reference to the pool

    private ItemSurface surface;

    private void Awake()
    {
        surface = GetComponent<ItemSurface>();
        if (foodPool == null)
            Debug.LogError("Food pool not assigned on " + name);
    }

    public void Interact()
    {
        if (surface.HasItem()) return;

        Debug.Log("Spawning food item from pool");

        // Get an object from the pool
        GameObject item = foodPool.getObject(); // uses default getObject method
        if (item == null)
        {
            Debug.LogWarning("No available objects in pool!");
            return;
        }

        // Snap it to the surface
        surface.SnapItem(item);
    }
}
