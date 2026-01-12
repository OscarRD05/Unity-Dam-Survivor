using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    public EnemyStats Stats;

    private int maxHP;
    private int currentHP;
    private int damage;
    private int defense;
    private float speed;

    public HealthBarUI healthBar;

    // Prefabs de orbes de experiencia
    public GameObject orbeVerde;   // 60%
    public GameObject orbeAzul;    // 30%
    public GameObject orbeDorado;  // 10%

    private float speedMultiplier = 1f;
    public float RotationSpeed = 10f;

    void Awake()
    {
        maxHP = Stats.MaxHP;
        currentHP = maxHP;
        damage = Stats.Damage;
        defense = Stats.Defense;
        speed = Stats.Speed;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHP);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direccion = player.transform.position - transform.position;
            direccion.Normalize();

            float velocidadActual = speed * speedMultiplier;
            transform.position += direccion * velocidadActual * Time.deltaTime;

            Vector3 direccionPlano = new Vector3(direccion.x, 0f, direccion.z);
            if (direccionPlano.sqrMagnitude > 0.001f)
            {
                Quaternion rotObjetivo = Quaternion.LookRotation(direccionPlano);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rotObjetivo,
                    RotationSpeed * Time.deltaTime
                );
            }
        }
    }

    public void SetSpeedMultiplier(float value)
    {
        speedMultiplier = value;
    }

    public void Recibirdano(int danio)
    {
        int danioFinal = danio - defense;
        if (danioFinal < 1) danioFinal = 1;

        currentHP -= danioFinal;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHP);
        }

        if (currentHP <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        SoltarOrbeXP();
        Destroy(gameObject);
    }

    private void SoltarOrbeXP()
    {
        // Si no hay orbes asignados, no hacemos nada
        if (orbeVerde == null && orbeAzul == null && orbeDorado == null)
            return;

        float r = Random.value; // 0.0 - 1.0
        GameObject orbeElegido = null;

        // 60% verde, 30% azul, 10% dorado
        if (r < 0.6f)
        {
            orbeElegido = orbeVerde;
        }
        else if (r < 0.9f)
        {
            orbeElegido = orbeAzul;
        }
        else
        {
            orbeElegido = orbeDorado;
        }

        if (orbeElegido != null)
        {
            Instantiate(orbeElegido, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.RecibirDmg(damage);
            }
        }
    }

}
