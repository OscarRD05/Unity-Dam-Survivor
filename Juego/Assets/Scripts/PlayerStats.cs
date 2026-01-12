using UnityEngine;
using UnityEngine.SceneManagement; // 👈 IMPORTANTE

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int ataque = 5;
    public int defensa = 0;

    private int currentHealth;
    private bool estaVivo = true;

    [Header("UI")]
    public HealthBarUI healthBar;

    // Referencia a la cámara (script Camara)
    private Camara camara;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // Inicializar barra de vida
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }

        // Buscar la cámara principal
        BuscarCamara();
    }

    void BuscarCamara()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            camara = cam.GetComponent<Camara>();
        }
    }

    public void RecibirDmg(int dmg)
    {
        if (!estaVivo) return;

        int dmgFinal = dmg - defensa;
        if (dmgFinal < 1) dmgFinal = 1;

        currentHealth -= dmgFinal;
        if (currentHealth < 0) currentHealth = 0;

        // Actualizar barra de vida
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // 🔥 Sacudir cámara
        if (camara == null)
        {
            BuscarCamara();
        }

        if (camara != null)
        {
            camara.Shake();
        }

        // ☠ MUERTE
        if (currentHealth <= 0)
        {
            estaVivo = false;
            Morir();
        }
    }

    void Morir()
    {
        // Cargar escena final
        SceneManager.LoadScene("Final");
    }
}
