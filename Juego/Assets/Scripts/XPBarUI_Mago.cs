using UnityEngine;
using UnityEngine.UI;

public class XPBarUI_Mago : MonoBehaviour
{
    public Image fillImage;
    public float xpActual = 0f;
    public float xpParaSubir = 100f;

    public int nivelActual = 1;

    public void AddXP(float amount)
    {
        xpActual += amount;

        if (xpActual >= xpParaSubir)
        {
            xpActual -= xpParaSubir;
            SubirNivel();
        }

        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (fillImage == null) return;

        float fill = xpActual / xpParaSubir;
        fillImage.fillAmount = Mathf.Clamp01(fill);
    }

    void SubirNivel()
    {
        nivelActual++;

        // 🔥 Aquí llamamos al LevelUpManager_Mago
        if (LevelUpMago.Instance != null)
        {
            LevelUpMago.Instance.OnLevelUp();
        }
        else
        {
            Debug.LogWarning("No hay LevelUpManager_Mago en la escena");
        }
    }
}
