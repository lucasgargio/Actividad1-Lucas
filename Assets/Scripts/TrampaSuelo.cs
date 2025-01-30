using UnityEngine;

public class TrampaSuelo : MonoBehaviour
{

    public int damageAmount = 20; // Cantidad de vida que quita

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Llama al método "Morir" del jugador
            SaludPersonaje saludPersonaje = other.GetComponent<SaludPersonaje>();
            if (saludPersonaje != null)
            {
                saludPersonaje.TakeDamage(damageAmount);
            }
        }
    }
}

