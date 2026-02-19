using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public bool IsHeld { get; private set; }

    public void SetHeld(bool held)
    {
        IsHeld = held;
    }
}
