using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public Pista pista;

    public string Interact()
    {
        //GameManager.Instance.inventoryManager.AddItem(pista.nombre, pista.sprite, pista.description);
        //GameManager.Instance.clues++;
        if (transform.CompareTag("Interactable")) Destroy(gameObject);
        else Destroy(this);

        return pista.nombre;
    }

}