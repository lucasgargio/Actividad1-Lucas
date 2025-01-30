using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método para cargar el juego
    public void Jugar()
    {
        // Asegúrate de reemplazar "NombreDeTuEscenaDeJuego" con el nombre real de tu escena de juego
        SceneManager.LoadScene("SampleScene");
    }

    // Método para salir del juego
    public void Salir()
    {
        Debug.Log("Salir del juego"); // Esto se verá en el Editor
        Application.Quit(); // Esto cerrará la aplicación al compilar

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
