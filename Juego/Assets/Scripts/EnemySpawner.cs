using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // <- importante para TextMeshPro

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    [SerializeField] private float spawnRadius = 10f;

    [Header("GameObjects")]
    [SerializeField] private Transform player;

    [SerializeField] private List<DataOleada> oleadas;

    [Header("UI")]
    public TMP_Text oleadaText;   // <- Aquí vinculas el texto del HUD

    private int numeroDeOleada = 0;

    void Start()
    {
        StartCoroutine(GenerarOleadas());
    }

    private IEnumerator spawn(DataOleada oleada)
    {
        for (int i = 0; i < oleada.CantidadDeEnemigos; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = player.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            Instantiate(oleada.EnemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(oleada.SpawnRate);
        }
    }

    public IEnumerator GenerarOleadas()
    {
        for (int i = 0; i < oleadas.Count; i++)
        {
            numeroDeOleada = i + 1; // empieza en 1, no en 0

            // Actualizar HUD
            if (oleadaText != null)
                oleadaText.text = "Oleada " + numeroDeOleada;

            // Ejecutar la oleada
            yield return StartCoroutine(spawn(oleadas[i]));

            // Espera entre oleadas
            if (oleadas[i].TiempoEntreOleadas > 0)
                yield return new WaitForSeconds(oleadas[i].TiempoEntreOleadas);
        }

        if (oleadaText != null)
            oleadaText.text = "¡Todas las oleadas completadas!";
    }
}
