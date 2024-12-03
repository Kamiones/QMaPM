using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] InventoryManager inventoryManager;

    //======== ITEM DATA ========//
    public string itemName;
    public Sprite itemSprite;
    public Sprite emptySprite;
    public string itemDescription;
    public bool isFull;


    //======== ITEM SLOT ========//
    [SerializeField] Image itemImage;
    
    public GameObject selectedShader;
    public bool thisItemSelected;

    //======== ITEM DESCRIPTION SLOT ========//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public void AddItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.color = new Color(1, 1, 1, 1);
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.color = new Color(1, 1, 1, 150f/255f);
            itemDescriptionImage.sprite = emptySprite;
        }
    }
}