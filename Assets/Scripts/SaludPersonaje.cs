using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SaludPersonaje : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound; // Referencia al AudioSource

    //public Transform respawnPoint; // Punto de reaparición
    public int vidaMaxima = 100;
    private int vidaActual;

    public Image fill;

    void Start()
    {
        vidaActual = vidaMaxima;
        UpdateHealthBar(); // Actualiza la barra al inicio
    }

    public void TakeDamage(int damage)
    {
        vidaActual -= damage;
        Debug.Log("Current Health: " + vidaActual);

        if (vidaActual < 0)
            vidaActual = 0;

        UpdateHealthBar();
        deathSound.Play();

        if (vidaActual <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (fill != null)
        {
            // Ajusta el porcentaje de relleno
            fill.fillAmount = (float) vidaActual / vidaMaxima;
        }
    }
    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        if (deathSound != null && deathSound.clip != null)
        {
            deathSound.Play();
            Invoke(nameof(LoadScene), deathSound.clip.length);
            //StartCoroutine(WaitAndLoadScene(deathSound.clip.length+3));
        }
        else
        {
            // Si no hay sonido, cargar la escena de inmediato
            LoadScene();
        }
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Esperar hasta que el sonido termine
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}

