using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("Datos del Boomerang")]
    public float speed = 10f;
    public float tiempoIda = 0.7f;
    public float tiempoVidaMax = 5f;
    public int damage = 25;

    [Header("Rebotes")]
    // Lo rellena el WeaponManager: nivelBoomerang
    // Nivel 1 = 1 rebote, Nivel 2 = 2 rebotes, etc.
    public int maxRebotes = 1;

    private Transform jugador;
    private bool regresando = false;
    private float timer = 0f;

    private int rebotesRestantes = 0;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, tiempoVidaMax);

        // Inicializamos los rebotes restantes según el nivel
        rebotesRestantes = Mathf.Max(0, maxRebotes);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tiempoIda)
            regresando = true;

        if (!regresando)
        {
            // Ida: sigue su forward inicial
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            // Vuelta: se dirige al jugador
            Vector3 direccion = (jugador.position - transform.position).normalized;
            transform.position += direccion * speed * Time.deltaTime;
            transform.forward = direccion;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.Recibirdano(damage);
        }

        // REBOTE EN EL JUGADOR
        if (regresando && other.CompareTag("Player"))
        {
            if (rebotesRestantes > 0)
            {
                // Rebota: vuelve a salir hacia delante
                rebotesRestantes--;
                regresando = false;
                timer = 0f; // vuelve a contar tiempo de ida
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
