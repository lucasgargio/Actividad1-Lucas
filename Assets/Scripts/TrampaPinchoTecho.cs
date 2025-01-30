using UnityEngine;

public class TrampaPinchoTecho : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int id;
    [SerializeField] private AudioSource trapSound;
    [SerializeField] private float limiteAltura = 15f;

    private Vector3 initialPosition;
    private bool activar;

    private void Start()
    {
        initialPosition = transform.position; // Guarda la posición inicial
    }

    private void OnEnable()
    {
        gM.OnNuevaInteraccion += Activar;
    }

    private void Activar(int id)
    {
        if (this.id == id)
        {
            activar = true;
            if (trapSound != null)
            {
                trapSound.Play();
            }
        }
    }

    private void Update()
    {
        if (activar)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime, Space.World);

            // Cuando la trampa ha caído completamente
            if (transform.position.y <= initialPosition.y - 5f)
            {
                Invoke(nameof(ResetTrap), 2f); // Espera 2 segundos antes de resetear
                activar = false;
            }
        }
    }

    private void ResetTrap()
    {
        transform.position = initialPosition; // Regresa la trampa a su lugar original
    }

    private void OnDisable()
    {
        gM.OnNuevaInteraccion -= Activar;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaludPersonaje saludPersonaje = other.GetComponent<SaludPersonaje>();
            if (saludPersonaje != null)
            {
                saludPersonaje.TakeDamage(saludPersonaje.vidaMaxima); // Mata al jugador
            }
        }
    }
}
