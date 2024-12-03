using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private CameraController cameraController;

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