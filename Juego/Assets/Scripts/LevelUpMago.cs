using UnityEngine;

public class LevelUpMago : MonoBehaviour
{
    public static LevelUpMago Instance;

    [Header("UI Level Up Mago")]
    public GameObject levelUpPanel;          // Panel que contiene las 3 cartas del mago

    [Header("Referencias")]
    public WeaponManager_Mago weaponManager; // Script de armas del mago

    private bool isChoosing = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);   // oculto al empezar
    }

    private void Update()
    {
        if (!isChoosing) return;

        // Elegir con teclas 1 / 2 / 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ElegirRayo();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ElegirOrbes();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ElegirFrost();
        }
    }

    /// <summary>
    /// Llamar desde la XP del mago cuando suba de nivel
    /// </summary>
    public void OnLevelUp()
    {
        if (levelUpPanel == null)
        {
            Debug.LogWarning("LevelUpManager_Mago: levelUpPanel no asignado");
            return;
        }

        Time.timeScale = 0f;
        isChoosing = true;
        levelUpPanel.SetActive(true);
    }

    // ================== CARTAS ==================

    public void ElegirRayo()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeRayo();

        CerrarPanel();
    }

    public void ElegirOrbes()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeOrbes();

        CerrarPanel();
    }

    public void ElegirFrost()
    {
        if (weaponManager != null)
            weaponManager.UnlockOrUpgradeFrost();

        CerrarPanel();
    }

    // ================== CERRAR PANEL ==================

    private void CerrarPanel()
    {
        isChoosing = false;
        Time.timeScale = 1f;

        if (levelUpPanel != null)
            levelUpPanel.SetActive(false);
    }

    // Para asignar a los botones de las cartas en el Canvas
    public void BotonRayo()  => ElegirRayo();
    public void BotonOrbes() => ElegirOrbes();
    public void BotonFrost() => ElegirFrost();
}
