using UnityEngine;

public class XpOrb : MonoBehaviour
{
    public float xpValue = 1f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null) return;   // no es un jugador

        // 1️⃣ Intentar encontrar barra del MAGO
        XPBarUI_Mago xpMago = FindObjectOfType<XPBarUI_Mago>();
        if (xpMago != null)
        {
            xpMago.AddXP(xpValue);
            Destroy(gameObject);
            return;
        }

        // 2️⃣ Si no hay barra del mago, usar la barra del guerrero
        XPBarUI xpWarrior = FindObjectOfType<XPBarUI>();
        if (xpWarrior != null)
        {
            xpWarrior.AddXP(xpValue);
            Destroy(gameObject);
            return;
        }

        Debug.LogWarning("No hay XPBar en esta escena.");
    }
}
