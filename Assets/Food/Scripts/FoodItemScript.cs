using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public enum FoodType
    {
        RawFish,
        ChoppedFish,
        Rice,
        Sushi
    }

    public FoodType foodType;

    public bool IsHeld { get; private set; }

    public void SetHeld(bool held)
    {
        IsHeld = held;
    }
}
