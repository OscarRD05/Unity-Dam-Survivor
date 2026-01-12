using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarCharacter : MonoBehaviour
{
    // Cargar la escena del juego
    public void Warrior()
    {
        SceneManager.LoadScene("Main"); 
    }

    public void Mago()
    {
        SceneManager.LoadScene("Main 1"); 
    }
}