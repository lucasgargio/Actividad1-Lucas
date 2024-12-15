using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoControl : MonoBehaviour
{
    // Variables públicas para configurar las velocidades
    public float factorGiro = 1.0f; // Factor de ajuste para la velocidad de giro
    public float factorAvance = 1.0f; // Factor de ajuste para la velocidad de avance

    public float velocidadGiroBase = 200.0f; // Velocidad de giro base (grados por segundo)
    public float velocidadDesplazamientoBase = 5.0f; // Velocidad de avance base (unidades por segundo)

    private CharacterController controlador; // Referencia al Character Controller

    void Start()
    {
        // Obtener el componente Character Controller del personaje
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Obtener la entrada del jugador para el giro y el desplazamiento
        float valorGiro = Input.GetAxis("Horizontal"); // Entrada de las teclas A/D o flechas izquierda/derecha
        float valorDesplazamiento = Input.GetAxis("Vertical"); // Entrada de las teclas W/S o flechas arriba/abajo

        // Calcular el ángulo de giro y aplicarlo al personaje
        float anguloGiro = valorGiro * velocidadGiroBase * factorGiro * Time.deltaTime;
        transform.Rotate(0, anguloGiro, 0); // Gira el personaje alrededor del eje Y

        // Calcular la dirección de movimiento en el espacio local
        Vector3 direccionMovimientoLocal = new Vector3(0, 0, valorDesplazamiento * velocidadDesplazamientoBase * factorAvance * Time.deltaTime);

        // Convertir la dirección de movimiento de espacio local a espacio mundial
        Vector3 direccionMovimientoMundial = transform.TransformDirection(direccionMovimientoLocal);

        // Mover al personaje usando el Character Controller
        controlador.Move(direccionMovimientoMundial);
    }


    // Detectar colisiones con objetos
    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador colisiona con el punto final
        if (other.CompareTag("Finish"))
        {
            // Cargar la escena de fin del juego
            SceneManager.LoadScene("FinDelJuego");
        }
    }


}
