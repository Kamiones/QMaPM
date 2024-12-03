using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    MovementController movementController;
    CameraController cameraController;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        if (movementController == null)
        {
            Debug.LogError("MovementController no encontrado en Player.");
        }
        else
        {
            Debug.Log("MovementController encontrado correctamente.");
        }

        if (transform.childCount > 1)
        {
            cameraController = GetComponentInChildren<CameraController>();
            if (cameraController == null)
            {
                Debug.LogError("CameraController no encontrado en el segundo hijo.");
            }
            else
            {
                Debug.Log("CameraController encontrado correctamente.");
            }
        }
        else
        {
            Debug.LogError("No hay suficientes hijos en Player para obtener CameraController.");
        }
    }

    void Start()
    {
        EnableInput(true);
    }

    public void EnableInput(bool value)
    {
        if (movementController != null)
        {
            movementController.enabled = value;
        }
        else
        {
            Debug.LogError("movementController es null al intentar habilitar la entrada.");
        }

        if (cameraController != null)
        {
            cameraController.enabled = value;
        }
        else
        {
            Debug.LogError("cameraController es null al intentar habilitar la entrada.");
        }
    }
}