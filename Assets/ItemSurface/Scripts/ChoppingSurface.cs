using UnityEngine;

public class ChoppingSurface : MonoBehaviour
{
    [SerializeField] private ObjectPoolComp choppedFishPool;
    [SerializeField] private ObjectPoolComp sushiPool;     

    private ItemSurface surface;

    private void Awake()
    {
        surface = GetComponent<ItemSurface>();
    }

    public void Interact()
    {
        if (!surface.HasItem()) return;

        GameObject currentItem = surface.GetCurrentItem();
        FoodItem food = currentItem.GetComponent<FoodItem>();
        if (food == null) return;

        if (food.foodType == FoodType.RawFish)
        {
            surface.ClearSurface();  
            GameObject chopped = choppedFishPool.getObject(surface.transform.position, surface.transform.rotation);
            if (chopped != null)
                surface.SnapItem(chopped);

            return;
        }

        if (food.foodType == FoodType.ChoppedFish)
        {
            Avatar_Move player = FindFirstObjectByType<Avatar_Move>();
            if (player == null) return;

            GameObject held = player.GetHeldObject();
            if (held == null) return;

            FoodItem heldFood = held.GetComponent<FoodItem>();
            if (heldFood == null) return;

            if (heldFood.foodType == FoodType.Rice)
            {
                surface.ClearSurface();
                held.SetActive(false);
                player.ClearHeldObject();

                GameObject sushi = sushiPool.getObject(surface.transform.position, surface.transform.rotation);
                if (sushi != null)
                    surface.SnapItem(sushi);
            }
        }
    }
}
