using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ItemSurface : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;

    private GameObject currentItem;

    [SerializeField] public List<FoodType> allowedFoodTypes;

    private void OnTriggerStay(Collider other)
    {
        // if already occupide ignore
        if (currentItem != null) return;

        if (!other.CompareTag("Ingredient")) return;

        FoodItem food = other.GetComponent<FoodItem>();
        if (food != null && food.IsHeld) return;

        if(!allowedFoodTypes.Contains(food.foodType)) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        SnapItem(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentItem)
        {
            currentItem = null;
        }
    }

    public void SnapItem(GameObject item)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();

        // stop all motion 
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;

     
        item.transform.position = snapPoint.position;
        item.transform.rotation = snapPoint.rotation;

        item.transform.SetParent(snapPoint);

        currentItem = item;
    }

    public void ClearSurface()
    {
        if (currentItem == null) return;

        currentItem.transform.SetParent(null);
        currentItem = null;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }

    public GameObject GetCurrentItem()
    {
        return currentItem;
    }


}
