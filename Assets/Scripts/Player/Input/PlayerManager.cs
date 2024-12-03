using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public MovementController movementController;
    [SerializeField] private CameraController cameraController;

    public void EnableInput(bool value)
    {
        movementController.enabled = value;
        cameraController.enabled = value;
    }

}