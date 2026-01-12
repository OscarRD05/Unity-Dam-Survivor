using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss")]
    public GameObject bossPrefab;     // El prefab del dragón
    public Transform spawnPoint;      // Dónde aparecerá (opcional)

    [Header("Tiempo de aparición")]
    public float tiempoParaAparecer = 600f; // 600 segundos = 10 minutos

    private float timer = 0f;
    private bool bossAparecido = false;

    void Update()
    {
        if (bossAparecido) return;

        timer += Time.deltaTime;

        if (timer >= tiempoParaAparecer)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        Vector3 pos;
        Quaternion rot;

        if (spawnPoint != null)
        {
            pos = spawnPoint.position;
            rot = spawnPoint.rotation;
        }
        else
        {
            // Si no asignas un spawnPoint, usa la posición de este objeto
            pos = transform.position;
            rot = transform.rotation;
        }

        Instantiate(bossPrefab, pos, rot);
        bossAparecido = true;
        Debug.Log("¡Boss spawneado a los 10 minutos!");
    }
}
