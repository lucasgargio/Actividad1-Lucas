using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // M�todo para cargar el juego
    public void Jugar()
    {
        // Aseg�rate de reemplazar "NombreDeTuEscenaDeJuego" con el nombre real de tu escena de juego
        SceneManager.LoadScene("SampleScene");
    }

    // M�todo para salir del juego
    public void Salir()
    {
        Debug.Log("Salir del juego"); // Esto se ver� en el Editor
        Application.Quit(); // Esto cerrar� la aplicaci�n al compilar

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
