using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 7.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;
    public Transform cameraTransform; // Referencia a la cámara

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento relativa a la cámara
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (move.magnitude >= 0.1f)
        {
            // Obtener el ángulo de rotación de la cámara en Y
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // Crear el vector de movimiento basado en la dirección de la cámara
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}