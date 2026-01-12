using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostZone : MonoBehaviour
{
    [Header("Base del efecto")]
    public float baseSlowFactor = 0.4f;     // 40% de velocidad en nivel 1
    public float baseIntervaloDanio = 0.5f; // cada cuánto hace daño en nivel 1
    public int danioPorTick = 1;
    public float baseDuracion = 4f;         // duración base nivel 1

    private float slowFactor;
    private float intervaloDanio;
    private float duracion;

    private int nivel = 1;
    private List<EnemyController> enemigosDentro = new List<EnemyController>();

    public void SetNivel(int n)
    {
        nivel = Mathf.Max(1, n);
    }

    private void Start()
    {
        // Más nivel → más duración y más ralentización, mayor frecuencia de daño
        slowFactor = Mathf.Clamp01(baseSlowFactor + 0.15f * (nivel - 1)); // sube 15% por nivel
        intervaloDanio = Mathf.Max(0.1f, baseIntervaloDanio / (1f + 0.2f * (nivel - 1)));
        duracion = baseDuracion * (1f + 0.5f * (nivel - 1)); // +50% duración por nivel

        StartCoroutine(DañoConstante());
        Destroy(gameObject, duracion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy == null) return;

        if (!enemigosDentro.Contains(enemy))
        {
            enemigosDentro.Add(enemy);
            enemy.SetSpeedMultiplier(slowFactor);       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy == null) return;

        if (enemigosDentro.Contains(enemy))
        {
            enemigosDentro.Remove(enemy);
            enemy.SetSpeedMultiplier(1f);               
        }
    }

    IEnumerator DañoConstante()
    {
        while (true)
        {
            for (int i = enemigosDentro.Count - 1; i >= 0; i--)
            {
                if (enemigosDentro[i] == null)
                {
                    enemigosDentro.RemoveAt(i);
                    continue;
                }

                enemigosDentro[i].Recibirdano(danioPorTick);
            }

            yield return new WaitForSeconds(intervaloDanio);
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemigosDentro)
        {
            if (enemy != null)
                enemy.SetSpeedMultiplier(1f);
        }
    }
}
