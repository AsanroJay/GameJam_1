using UnityEngine;

public class FoodSpawnerSurface : MonoBehaviour
{
    private ObjectPoolComp pool; 

    private ItemSurface surface;

    private void Awake()
    {
        surface = GetComponent<ItemSurface>();
        pool = GetComponent<ObjectPoolComp>();
    }
    public void Interact()
    {
        if (surface.HasItem()) return;

        Debug.Log("Spawning food item from pool");

        GameObject item = pool.getObject(); 
        if (item == null)
        {
            Debug.LogWarning("No available objects in pool!");
            return;
        }

        item.SetActive(true);

        surface.SnapItem(item);
    }
}
