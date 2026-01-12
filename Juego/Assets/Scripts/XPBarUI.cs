using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    public Image fillImage;
    public float xpActual = 0f;
    public float xpParaSubir = 100f;

    public int nivelActual = 1;

    public void AddXP(float amount)
    {
        xpActual += amount;

        // Si sobrepasa, guardamos el sobrante
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
        // 🔥 Avisar al LevelUpManager
        if (LevelUpManager.Instance != null)
        {
            LevelUpManager.Instance.OnLevelUp();
        }
        else
        {
        }
    }
}
