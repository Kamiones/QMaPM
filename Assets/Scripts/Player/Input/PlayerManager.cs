using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    MovementController movementController;
    CameraController cameraController;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        cameraController = transform.GetChild(1).GetComponent<CameraController>();
    }

    void Start()
    {
        EnableInput(true);
    }

    public void EnableInput(bool value)
    {
        movementController.enabled = value;
        cameraController.enabled = value;
    }
}