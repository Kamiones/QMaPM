using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] string itemName;
    [SerializeField] Sprite itemSprite;

    [TextArea]
    [SerializeField] string itemDescription;



    public string Interact()
    {
        inventoryManager.AddItem(itemName, itemSprite, itemDescription);
        
        if (transform.CompareTag("Interactable")) Destroy(gameObject);
        else Destroy(this);

        return itemName;
    }
}