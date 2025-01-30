using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idPuerta;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private float limiteY = -5f; // Posición Y límite para destruir la puerta

    private bool abrir = false;
    // Start is called before the first frame update
    void Start()
    {
        gM.OnBotonPulsado += Abrir;
    }

    private void Abrir(int idBotonPulsado)
    {
        if (idBotonPulsado == idPuerta)
        {
            abrir = true;

            if (doorSound != null)
            {
                doorSound.Play();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (abrir)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime);

            if (transform.position.y <= limiteY)
            {
                Destroy(gameObject); // Destruir la puerta
            }
        }
    }
}
