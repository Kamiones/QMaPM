using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    //private bool menuActivated;
    public ItemSlot[] itemSlot;

    void Update()
    {
        // ...existing code...
        // Eliminar la lógica de entrada aquí para gestionar la entrada desde PlayerInventory
    }

    public void ShowInventory()
    {
        InventoryMenu.SetActive(true);
        //menuActivated = true;
        DeselectAllSlots();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideInventory()
    {
        InventoryMenu.SetActive(false);
        //menuActivated = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemSprite, itemDescription);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}