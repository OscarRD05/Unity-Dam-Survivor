using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    [Header("Prefabs de armas")]
    public GameObject hachaPrefab;
    public GameObject boomerangPrefab;
    public GameObject slashPrefab;

    [Header("Fire Rates")]
    public float hachaRate = 1.5f;
    public float boomerangRate = 3f;
    public float slashRate = 2f;

    [Header("Niveles de armas")]
    public int nivelHacha = 1;      // Empiezas con hacha nivel 1
    public int nivelBoomerang = 0;  // 0 = bloqueada
    public int nivelSlash = 0;      // 0 = bloqueada

    [Header("Hacha ráfaga")]
    public float tiempoEntreHachas = 0.2f;   // 0.2 segundos entre cada hacha de la ráfaga

    void Start()
    {
        StartCoroutine(HachaRoutine());
        StartCoroutine(BoomerangRoutine());
        StartCoroutine(SlashRoutine());
    }

    // ================== H A C H A ==================
    IEnumerator HachaRoutine()
    {
        while (true)
        {
            if (nivelHacha > 0 && hachaPrefab != null)
            {
                // Lanza una ráfaga de "nivelHacha" proyectiles
                StartCoroutine(RafagaHachas());
            }

            yield return new WaitForSeconds(hachaRate);
        }
    }

    IEnumerator RafagaHachas()
    {
        int cantidad = Mathf.Max(1, nivelHacha); // Nivel 1 = 1 hacha, nivel 3 = 3 hachas, etc.

        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(hachaPrefab, transform.position, transform.rotation);

            if (i < cantidad - 1)
                yield return new WaitForSeconds(tiempoEntreHachas);
        }
    }

    // ================== B O O M E R A N G ==================
    IEnumerator BoomerangRoutine()
    {
        while (true)
        {
            if (nivelBoomerang > 0 && boomerangPrefab != null)
            {
                GameObject b = Instantiate(boomerangPrefab, transform.position, transform.rotation);

                // Pasar nivel al boomerang para que sepa cuántos rebotes tiene
                Boomerang boomScript = b.GetComponent<Boomerang>();
                if (boomScript != null)
                {
                    boomScript.maxRebotes = Mathf.Max(1, nivelBoomerang);
                }
            }

            yield return new WaitForSeconds(boomerangRate);
        }
    }

    // ================== S L A S H ==================
    IEnumerator SlashRoutine()
    {
        while (true)
        {
            if (nivelSlash > 0 && slashPrefab != null)
            {
                GameObject s = Instantiate(slashPrefab, transform.position, transform.rotation);

                // Pasar nivel al slash para que escale el collider
                SlashAtaque slashScript = s.GetComponent<SlashAtaque>();
                if (slashScript != null)
                {
                    slashScript.nivelSlash = nivelSlash;
                }
            }

            yield return new WaitForSeconds(slashRate);
        }
    }

    // ================== DESBLOQUEAR / MEJORAR ==================

    // Si no la tienes -> nivel 1. Si ya la tienes -> +1 nivel
    public void UnlockOrUpgradeHacha()
    {
        if (nivelHacha == 0) nivelHacha = 1;
        else nivelHacha++;

        Debug.Log("Hacha nivel " + nivelHacha);
    }

    public void UnlockOrUpgradeBoomerang()
    {
        if (nivelBoomerang == 0) nivelBoomerang = 1;
        else nivelBoomerang++;

        Debug.Log("Boomerang nivel " + nivelBoomerang);
    }

    public void UnlockOrUpgradeSlash()
    {
        if (nivelSlash == 0) nivelSlash = 1;
        else nivelSlash++;

        Debug.Log("Slash nivel " + nivelSlash);
    }
}
