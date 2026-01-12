using UnityEngine;
using System.Collections.Generic;

public class SlashAtaque : MonoBehaviour
{
    public int dano = 10;
    public float tiempoDeVida = 0.2f;

    [Header("Nivel del Slash")]
    // Lo rellena el WeaponManager: nivelSlash
    public int nivelSlash = 1;

    private HashSet<EnemyController> enemigosGolpeados;
    private BoxCollider boxCol;
    private Vector3 tamañoBase;

    void Awake()
    {
        enemigosGolpeados = new HashSet<EnemyController>();
        boxCol = GetComponent<BoxCollider>();
        if (boxCol != null)
            tamañoBase = boxCol.size;
    }

    void Start()
    {
        // Por si acaso, nunca menos de nivel 1
        nivelSlash = Mathf.Max(1, nivelSlash);

        AplicarNivel();  // escalado del collider
        Destroy(gameObject, tiempoDeVida);
    }

    void AplicarNivel()
    {
        if (boxCol == null) return;

        // Aumentar lateralmente (X) y hacia adelante (Z)
        // Ejemplo: cada nivel aumenta un 30%
        float factor = 1f + 0.3f * (nivelSlash - 1);

        Vector3 nuevo = tamañoBase;
        nuevo.x *= factor; // más ancho
        nuevo.z *= factor; // más largo

        boxCol.size = nuevo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemigo = other.GetComponent<EnemyController>();

            if (enemigo != null && !enemigosGolpeados.Contains(enemigo))
            {
                enemigo.Recibirdano(dano);
                enemigosGolpeados.Add(enemigo);
            }
        }
    }
}
