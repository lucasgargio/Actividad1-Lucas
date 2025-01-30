/*using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoControl : MonoBehaviour
{
    [SerializeField] private float vidaInicial;
    [SerializeField] private CanvasManager canvas;
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private AudioSource footstepAudio; // Referencia al AudioSource de los pasos
    // Variables públicas para configurar las velocidades
    public float factorGiro = 1.0f; // Factor de ajuste para la velocidad de giro
    public float factorAvance = 1.0f; // Factor de ajuste para la velocidad de avance

    public float velocidadGiroBase = 200.0f; // Velocidad de giro base
    public float velocidadDesplazamientoBase = 5.0f; // Velocidad de avance base
                                                     // Variables para el salto
    public float fuerzaSalto = 8.0f; // Fuerza del salto
    public float gravedad = 20.0f; // Gravedad 
    private float velocidadVertical = 0.0f;

    private CharacterController controlador; // Referencia al Character Controller
    private float vidaActual;
    private bool estaVivo = true;


    void Start()
    {
        vidaActual = vidaInicial;
        // Obtener el componente Character Controller del personaje
        controlador = GetComponent<CharacterController>();
        if (controlador == null)
        {
            Debug.LogError("¡No se encontró el componente CharacterController en el objeto!");
        }

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

        // Aplicar gravedad y salto
        if (controlador.isGrounded)
        {
            velocidadVertical = 0; // Reinicia la velocidad vertical al estar en el suelo

            if (Input.GetButtonDown("Jump")) // Detecta si el jugador presiona el botón de salto
            {
                velocidadVertical = fuerzaSalto; // Aplica la fuerza de salto
            }
        }
        else
        {
            velocidadVertical -= gravedad * Time.deltaTime; // Aplica gravedad mientras está en el aire
        }

        // Control del sonido de pasos
        if (valorDesplazamiento != 0 && controlador.isGrounded)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
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

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.TryGetComponent(out TrampaPinchoTecho trampaPinchoTecho))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Morir()
    {
        if (estaVivo)
        {
            estaVivo = false;  // Desactivar el movimiento del jugador
            Debug.Log("¡El jugador ha muerto!");
            SceneManager.LoadScene("SampleScene");  // Reiniciar la escena
        }
    }
   


    /*  private void OnCollisionEnter(Collision other)
      {
          if (other.gameObject.CompareTag("Cubo"))
          {
              vidaActual -= 25;
              canvas.Barradevida.fillAmount = vidaActual / vidaInicial;
          }
      }

}*/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoControl : MonoBehaviour
{
    //[SerializeField] private CanvasManager canvas;
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private AudioSource footstepAudio;

    // Variables públicas para configurar las velocidades
    public float factorGiro = 1.0f;
    public float factorAvance = 1.0f;
    public float velocidadGiroBase = 200.0f;
    public float velocidadDesplazamientoBase = 5.0f;

    // Variables para el salto
    public float fuerzaSalto = 15.0f;
    public float gravedad = 9.8f;
    private float velocidadVertical = 0.0f;

    private CharacterController controlador;
   
    private bool puedeSaltar = true; // Controla si el personaje puede saltar
    private float tiempoEntreSaltos = 0.0001f; // Tiempo mínimo entre saltos

    void Start()
    {
        // Obtener el componente Character Controller del personaje
        controlador = GetComponent<CharacterController>();
        if (controlador == null)
        {
            Debug.LogError("¡No se encontró el componente CharacterController en el objeto!");
        }
    }

    void Update()
    {
        // Movimiento horizontal y rotación
        float valorGiro = Input.GetAxis("Horizontal");
        float valorDesplazamiento = Input.GetAxis("Vertical");

        // Rotación del personaje (solo si hay entrada de giro)
        if (Mathf.Abs(valorGiro) > 0.01f)
        {
            float anguloGiro = valorGiro * velocidadGiroBase * factorGiro * Time.deltaTime;
            transform.Rotate(0, anguloGiro, 0);
        }

        // Movimiento horizontal y hacia adelante
        Vector3 direccionMovimientoLocal = new Vector3(valorGiro * velocidadDesplazamientoBase * factorAvance, 0, valorDesplazamiento * velocidadDesplazamientoBase * factorAvance);
        Vector3 direccionMovimientoMundial = transform.TransformDirection(direccionMovimientoLocal);

        // Aplicar gravedad y salto
        if (controlador.isGrounded)
        {
            velocidadVertical = -0.5f; // Ligero valor negativo para mantener al personaje en el suelo

            // Permitir salto solo si el tiempo entre saltos se cumple
            if (Input.GetButtonDown("Jump") && puedeSaltar)
            {
                velocidadVertical = fuerzaSalto; // Aplica la fuerza de salto
                puedeSaltar = false; // Desactiva temporalmente la capacidad de salto
                Invoke(nameof(HabilitarSalto), tiempoEntreSaltos); // Reactiva el salto después de un breve tiempo
            }
        }
        else
        {
            velocidadVertical -= gravedad * Time.deltaTime; // Aplica la gravedad mientras está en el aire
        }

        // Incluir el movimiento vertical en la dirección final
        direccionMovimientoMundial.y = velocidadVertical;

        // Corregir el salto independiente de la rotación de la cámara
        //if (Input.GetButtonDown("Jump") && controlador.isGrounded)
        //{
        //    direccionMovimientoMundial.y = fuerzaSalto;
        //}

        // Mover al personaje usando el Character Controller
        controlador.Move(direccionMovimientoMundial * Time.deltaTime);

        // Sonido de pasos
        if (valorDesplazamiento != 0 && controlador.isGrounded)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    private void HabilitarSalto()
    {
        puedeSaltar = true; // Permitir que el personaje salte nuevamente
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("KillZone")) // Asegura que la trampa tenga este tag
        {
            SceneManager.LoadScene("SampleScene"); // Recarga la escena
        }
        
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("FinDelJuego");
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        //if (other.transform.TryGetComponent(out TrampaPinchoTecho trampaPinchoTecho))
        //{
        //    SceneManager.LoadScene("SampleScene");
        //}
    }


}







