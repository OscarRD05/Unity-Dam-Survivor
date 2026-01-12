using UnityEngine;

public class OrbeDanio : MonoBehaviour
{
    [Header("Daño")]
    public int danioBase = 5;
    private int danio;

    [Header("Órbita")]
    public float baseSpeed = 120f;  // velocidad de giro en nivel 1
    private float speed;
    
    private int nivel = 1;
    private Transform centro;       // normalmente, el mago (parent)

    void Start()
    {
        // El centro suele ser el padre (el mago)
        centro = transform.parent;
        if (centro == null)
        {
            Debug.LogWarning("OrbeDanio: no tengo padre, no puedo orbitar.");
        }

        ActualizarPorNivel();
    }

    // Llamado desde WeaponManager_Mago
    public void SetNivel(int n)
    {
        nivel = Mathf.Max(1, n);
        ActualizarPorNivel();
    }

    void ActualizarPorNivel()
    {
        // Daño escala con el nivel (opcional)
        danio = danioBase * nivel;

        // Velocidad de giro: +20% por nivel
        speed = baseSpeed * (1f + 0.20f * (nivel - 1));
    }

    void Update()
    {
        if (centro == null) return;

        // Girar alrededor del mago
        transform.RotateAround(centro.position, Vector3.up, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.Recibirdano(danio);
        }
    }
}
