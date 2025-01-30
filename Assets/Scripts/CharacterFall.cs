using UnityEngine;

public class CharacterFall : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float gravity = 9.8f; // Gravedad
    private Vector3 velocity; // Velocidad del personaje
    private CharacterController controller; // Referencia al Character Controller

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento básico
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        // Aplicar gravedad si no está en el suelo
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime; // Aumentar la velocidad de caída
        }
        else
        {
            velocity.y = -2f; // Valor pequeño para mantenerlo en el suelo
        }

        // Aplicar movimiento vertical (caída)
        controller.Move(velocity * Time.deltaTime);
    }
}

