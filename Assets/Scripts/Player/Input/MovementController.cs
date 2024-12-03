using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] public float speed = 7.0f;
    [SerializeField] public float jumpHeight = 1.0f;
    private float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
    }
}