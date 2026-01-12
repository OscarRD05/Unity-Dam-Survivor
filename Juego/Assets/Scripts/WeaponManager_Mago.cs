using UnityEngine;
using System.Collections;

public class WeaponManager_Mago : MonoBehaviour
{
    [Header("Prefabs de armas")]
    public GameObject rayoPrefab;       // Prefab con script Rayo
    public GameObject orbePrefab;       // Prefab con OrbeDanio + OrbeOrbitador
    public GameObject frostZonePrefab;  // Prefab con script FrostZone

    [Header("Ritmos de disparo")]
    public float rayoRate = 2f;         // Cada cuánto lanza rayo
    public float frostRate = 5f;        // Cada cuánto genera una FrostZone

    [Header("Niveles de armas")]
    public int nivelRayo = 1;      // El mago empieza con rayo nivel 1
    public int nivelOrbes = 0;     // 0 = sin escudo orbital
    public int nivelFrost = 0;     // 0 = sin FrostZone

    [Header("Escudo Orbital")]
    public float radioOrbes = 1.5f; // Distancia al mago

    void Start()
    {
        // Comenzar sistemas de armas
        StartCoroutine(RayoRoutine());
        StartCoroutine(FrostRoutine());

        // Si ya tuviera nivel de orbes al empezar
        if (nivelOrbes > 0 && orbePrefab != null)
        {
            GenerarOrbes();
        }
    }

    // =============================================================
    //                           R A Y O
    // =============================================================

    IEnumerator RayoRoutine()
    {
        while (true)
        {
            if (nivelRayo > 0 && rayoPrefab != null)
            {
                // posición de salida (frente al mago)
                Vector3 offset = transform.forward * 0.8f + Vector3.up * 0.5f;

                GameObject r = Instantiate(
                    rayoPrefab,
                    transform.position + offset,
                    transform.rotation
                );

                // 🔥 MUY IMPORTANTE: hacerlo hijo del mago
                r.transform.SetParent(transform);

                // Pasar nivel al rayo (si usas SetNivel)
                Rayo rayoScript = r.GetComponent<Rayo>();
                if (rayoScript != null)
                {
                    rayoScript.SetNivel(nivelRayo);
                }
            }

            yield return new WaitForSeconds(rayoRate);
        }
    }


    public void UnlockOrUpgradeRayo()
    {
        if (nivelRayo == 0) nivelRayo = 1;
        else nivelRayo++;
    }

    // =============================================================
    //                           O R B E S
    // =============================================================

   void GenerarOrbes()
    {
        // Borrar orbes anteriores
        OrbeDanio[] existentes = GetComponentsInChildren<OrbeDanio>();
        foreach (var o in existentes)
        {
            Destroy(o.gameObject);
        }

        int nivel = Mathf.Max(1, nivelOrbes);

        // 👇 Fórmula:
        // Nivel 1–2 → 3 orbes
        // Nivel 3–4 → 4 orbes
        // Nivel 5–6 → 5 orbes...
        int cantidad = 3 + Mathf.FloorToInt((nivel - 1) / 2f);

        float anguloBase = 360f / cantidad;

        for (int i = 0; i < cantidad; i++)
        {
            GameObject orbe = Instantiate(orbePrefab, transform);

            Quaternion rot = Quaternion.Euler(0, anguloBase * i, 0);
            Vector3 posLocal = rot * new Vector3(radioOrbes, 0f, 0f);
            orbe.transform.localPosition = posLocal;

            OrbeDanio od = orbe.GetComponent<OrbeDanio>();
            if (od != null)
                od.SetNivel(nivelOrbes); // daño + velocidad de giro según nivel
        }
    }

    public void UnlockOrUpgradeOrbes()
    {
        if (nivelOrbes == 0) nivelOrbes = 1;
        else nivelOrbes++;

        if (orbePrefab != null)
            GenerarOrbes();
    }


    // =============================================================
    //                        F R O S T  Z O N E
    // =============================================================

   IEnumerator FrostRoutine()
    {
        while (true)
        {
            if (nivelFrost > 0 && frostZonePrefab != null)
            {
                Debug.Log("[Mago] Lanza FrostZone nivel " + nivelFrost);

                // ⬇️ EXACTAMENTE donde está el mago (incluyendo Y)
                Vector3 pos = transform.position;
                Instantiate(frostZonePrefab, pos, Quaternion.identity);
            }

            yield return new WaitForSeconds(frostRate);
        }
    }

    public void UnlockOrUpgradeFrost()
    {
        if (nivelFrost == 0) nivelFrost = 1;
        else nivelFrost++;
    }
}
