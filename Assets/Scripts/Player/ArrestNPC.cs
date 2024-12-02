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
    private Camera mainCamera;
    public Text interactionText; // Referencia al texto UI
    private bool isArresting = false;

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena!");
        }
    }

    void Update()
    {
        if (isArresting) return;

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, arrestDistance))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                NPC npc = hit.collider.GetComponent<NPC>();
                if (npc != null)
                {
                    npcToArrest = hit.collider.gameObject;
                    if (cluesFound > 0)
                    {
                        ShowMessage($"Presiona E para arrestar a {npc.npcName}");
                    }
                    else
                    {
                        ShowMessage("Necesitas al menos una pista para arrestar");
                    }
                }
            }
        }
        else
        {
            npcToArrest = null;
            HideMessage();
        }

        if (Input.GetKeyDown(KeyCode.E) && npcToArrest != null && cluesFound > 0)
        {
            Arrest(npcToArrest);
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

    private void HideMessage()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
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
