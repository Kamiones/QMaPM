using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    [SerializeField] float maxDistance = 2f;
    [SerializeField] GameObject dialogArea;
    [SerializeField] TMP_Text dialogText;

    bool isInteracting;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInteracting)
        {
            dialogArea.SetActive(false);
            isInteracting = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                Debug.Log("Interact [E]");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isInteracting = true;
                    dialogText.text = "Encontraste: " + hit.transform.GetComponent<Item>().Interact();
                    dialogArea.SetActive(true);
                }
            }
            else if (hit.transform.CompareTag("NPC"))
            {
                Debug.Log("Interact [E]");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isInteracting = true;
                    try
                    {
                        dialogText.text = hit.transform.GetComponent<Item>().Interact();
                        dialogArea.SetActive(true);
                    }
                    catch {}
                }
            }
        }
    }
}