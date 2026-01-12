using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 5f;

    private Vector3 direction;

    void Start()
    {
        // Buscar al jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Dirección hacia el jugador desde la posición actual
            direction = (player.transform.position - transform.position).normalized;

            // Opcional: hacer que la bola mire hacia donde va
            Vector3 dirPlano = direction;
            dirPlano.y = 0f;
            if (dirPlano.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dirPlano);
            }
        }
        else
        {
            // Si no encuentra jugador, va hacia adelante
            direction = transform.forward;
        }

        // Se destruye sola después de X segundos
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mover en línea recta hacia la dirección calculada
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo nos importa el jugador, de momento ignoramos todo lo demás
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.RecibirDmg(damage);
            }

            Destroy(gameObject);
        }
    }
}
