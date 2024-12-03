using UnityEngine;

public class Item : MonoBehaviour
{
    private InventoryManager inventoryManager;
    [HideInInspector] public Pista pista;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public string Interact()
    {
        inventoryManager.AddItem(pista.nombre, pista.sprite, pista.description);
        GameManager.Instance.clues++;
        if (transform.CompareTag("Interactable")) Destroy(gameObject);
        else Destroy(this);

        return pista.nombre;
    }

}