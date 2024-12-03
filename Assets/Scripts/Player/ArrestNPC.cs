using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ArrestNPC : MonoBehaviour
{
    public float arrestDistance = 2f; // Distancia máxima de arresto
    private GameObject npcToArrest;
    public int cluesFound = 0;
    private GameManager gameManager;
    public Text interactionText; // Referencia al texto UI
    private bool isArresting = false;

    [SerializeField] private MovementController movementController;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena!");
        }
    }

    public bool IsSusInFront()
    {
        RaycastHit hit;
        // Offset para evitar que el raycast detecte al propio jugador
        Vector3 origin = transform.position + transform.forward * 0.1f; 
        if (Physics.Raycast(origin, transform.forward, out hit, arrestDistance))
        {
            bool IsSospechoso = hit.collider.CompareTag("Sospechoso");
            Debug.Log($"IsSospechoso: {IsSospechoso}, Hit: {hit.collider.name}");
            if (IsSospechoso)
            {
                npcToArrest = hit.collider.gameObject;
            }
            else
            {
                npcToArrest = null;
            }
                return IsSospechoso;
            }
        Debug.Log("IsSospechoso: false, No hit");
        return false;
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsSusInFront() && cluesFound > 0 && movementController.IsGrounded)
            {

                Arrest(npcToArrest);
            }
            else
            {
                ShowMessage("No puedes arrestar en este momento.");
            }
        }
    }


    private void HideMessage()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }


    private void ShowMessage(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void Arrest(GameObject npc)
    {
        isArresting = true;
        NPC npcComponent = npc.GetComponent<NPC>();
        
        if (npcComponent != null)
        {
            if (npcComponent.isGuilty)
            {
                gameManager.EndGame(true); // Victoria
                ShowMessage("¡Has atrapado al culpable!");
            }
            else
            {
                gameManager.EndGame(false); // Derrota
                ShowMessage("¡Has arrestado a un inocente!");
            }
        }
    }
}
