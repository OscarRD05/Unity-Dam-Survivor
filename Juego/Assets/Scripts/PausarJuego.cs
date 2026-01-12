using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    private bool juegoPausado = false;

    void Start()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        menuPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                Pausar();
        }
    }

    public void ReanudarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}