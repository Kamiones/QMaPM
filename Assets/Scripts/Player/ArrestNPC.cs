using UnityEngine;
using UnityEngine.UI;

public class ArrestNPC : MonoBehaviour
{
    public float arrestDistance = 2f; // Distancia máxima de arresto
    private GameObject npcToArrest;
    public int cluesFound = 0;
    public Text interactionText; // Referencia al texto UI
    private bool isArresting = false;

    [SerializeField] private MovementController movementController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsSusInFront() && cluesFound > 0 && movementController.IsGrounded) GameManager.Instance.Arrest(npcToArrest.GetComponent<Susss>().sus);
            else ShowMessage("No puedes arrestar en este momento.");
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
            npcToArrest = IsSospechoso? hit.collider.gameObject : null;
            return IsSospechoso;
        }
        Debug.Log("IsSospechoso: false, No hit");
        return false;
    }

    private void HideMessage()
    {
        if (interactionText != null) interactionText.gameObject.SetActive(false);
    }

    private void ShowMessage(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

}