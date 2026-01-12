using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Cargar la escena del juego
    public void Jugar()
    {
        SceneManager.LoadScene("Seleccionar Character"); 
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}