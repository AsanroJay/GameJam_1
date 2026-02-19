using UnityEngine;

public class ChoppingSurface : MonoBehaviour
{
    [SerializeField] private GameObject choppedFishPrefab;
    [SerializeField] private GameObject sushiPrefab;

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

        if (food.foodType == FoodItem.FoodType.RawFish)
        {
            surface.ClearSurface();
            Destroy(currentItem);

            GameObject chopped = Instantiate(choppedFishPrefab);
            surface.SnapItem(chopped);
            return;
        }

    
        if (food.foodType == FoodItem.FoodType.ChoppedFish)
        {
            Avatar_Move player = FindObjectOfType<Avatar_Move>();

            if (player == null) return;

            GameObject held = player.GetHeldObject();
            if (held == null) return;

            FoodItem heldFood = held.GetComponent<FoodItem>();
            if (heldFood == null) return;

            if (heldFood.foodType == FoodItem.FoodType.Rice)
            {
                // remove both ingredients
                surface.ClearSurface();
                Destroy(currentItem);
                Destroy(held);

                player.ClearHeldObject();

                // spawn sushi
                GameObject sushi = Instantiate(sushiPrefab);
                surface.SnapItem(sushi);
            }
        }
    }
}
