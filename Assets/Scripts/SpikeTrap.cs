using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject trampa; // La trampa que caerá

    // Detectar cuando algo entra en la zona de la trampa
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {   
            Debug.Log("El jugador ha activado la trampa de pinchos de techo.");

            // Activar la trampa (habilitar gravedad)
            Rigidbody rb = trampa.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Permitir que la trampa caiga
            }

        }
    }
}

