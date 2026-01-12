using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rayo : MonoBehaviour
{
    [Header("Daño base")]
    public float duracion = 1.5f;          // tiempo visible antes de destruirse
    public int danioPorTick = 2;

    [Header("Ticks")]
    public float baseIntervaloDanio = 0.25f;
    public float intervaloMinimo = 0.05f;

    [Header("Ancho visual")]
    public float baseAncho = 1f;       // escala X base
    public float anchoPorNivel = 0.3f; // +30% por nivel

    private float intervaloDanio;
    private int nivel = 1;
    private Vector3 escalaBase;

    private List<EnemyController> enemigosDentro = new List<EnemyController>();

    private void Awake()
    {
        escalaBase = transform.localScale;
        intervaloDanio = baseIntervaloDanio;
    }

    // Llamado por WeaponManager_Mago
    public void SetNivel(int n)
    {
        nivel = Mathf.Max(1, n);

        // Más golpes: reducimos el intervalo entre ticks
        float factorVelocidad = 1f + 0.3f * (nivel - 1);   // L1=1, L2=1.3, L3=1.6...
        intervaloDanio = Mathf.Max(intervaloMinimo, baseIntervaloDanio / factorVelocidad);

        // Más ancho visual
        float factorAncho = 1f + anchoPorNivel * (nivel - 1);
        transform.localScale = new Vector3(
            escalaBase.x * factorAncho,
            escalaBase.y,
            escalaBase.z
        );
    }

    private void Start()
    {
        StartCoroutine(DañoConstante());
        Destroy(gameObject, duracion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null && !enemigosDentro.Contains(enemy))
                enemigosDentro.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null && enemigosDentro.Contains(enemy))
                enemigosDentro.Remove(enemy);
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
}
