using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject victoriaPanel;  // UI con "¡VICTORIA!"

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Opcional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnBossDefeated()
    {
        Debug.Log("🎉 VICTORIA: Boss derrotado");

        if (victoriaPanel != null)
            victoriaPanel.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego (opcional)
    }
}
