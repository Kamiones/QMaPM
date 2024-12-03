using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [HideInInspector] public Pista pista;

    public string Interact()
    {
        inventoryManager.AddItem(pista.nombre, pista.sprite, pista.description);
        
        if (transform.CompareTag("Interactable")) Destroy(gameObject);
        else Destroy(this);

        return pista.nombre;
    }

}