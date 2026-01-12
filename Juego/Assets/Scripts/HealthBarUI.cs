using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;   // << arrastra el "Fill" del asset aquí
    
    private int maxHealth;

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        SetHealth(max); // pone vida completa al iniciar
    }

    public void SetHealth(int hp)
    {
        if (maxHealth <= 0) return;
        fillImage.fillAmount = (float)hp / maxHealth;
    }
}
