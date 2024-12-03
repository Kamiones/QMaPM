using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private CameraController cameraController;

    public void EnableInput(bool value)
    {
        movementController.enabled = value;
        cameraController.enabled = value;
    }

}