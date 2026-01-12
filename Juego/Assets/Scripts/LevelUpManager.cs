using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance;

    [Header("UI Level Up")]
    public GameObject levelUpPanel;   // Panel que contiene las 3 tarjetas

    [Header("Armas")]
    public WeaponManager weaponManager;  // Script que activa hacha/boomerang/slash

    private bool isChoosing = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);  // 👈 aseguramos oculto al inicio
    }

    void Update()
    {
        if (!isChoosing) return;

        // Elegir con teclas 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ElegirHacha();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ElegirBoomerang();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ElegirSlash();
        }
    }

    public void OnLevelUp()
    {
        if (levelUpPanel == null)
        {
            return;
        }

        // Pausar juego
        Time.timeScale = 0f;
        isChoosing = true;

        levelUpPanel.SetActive(true);
    }

    public void ElegirHacha()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeHacha();

        CerrarPanel();
    }

    public void ElegirBoomerang()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeBoomerang();

        CerrarPanel();
    }

    public void ElegirSlash()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeSlash();

        CerrarPanel();
    }
    void CerrarPanel()
    {
        isChoosing = false;
        Time.timeScale = 1f;

        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);
    }

    // Para botones de las tarjetas, si los usas
    public void BotonHacha()     => ElegirHacha();
    public void BotonBoomerang() => ElegirBoomerang();
    public void BotonSlash()     => ElegirSlash();
}







