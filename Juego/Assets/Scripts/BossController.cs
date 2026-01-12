using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Stats")]
    public EnemyStats Stats;
    public HealthBarUI healthBar;      // barra de vida del boss (worldspace o UI)

    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackRange = 10f;    // rango para empezar a atacar

    [Header("Ataque (Fireballs)")]
    public GameObject fireballPrefab;
    public Transform firePoint;        // desde donde dispara
    public float attackInterval = 3f;  // tiempo entre ráfagas
    public float timeBetweenFireballs = 0.2f; // tiempo entre cada bola dentro de la ráfaga

    private Transform player;
    private int maxHP;
    private int currentHP;

    private bool isAttacking = false;
    private int volleyCount = 1;       // 1ª vez 1 bola, luego 2, luego 3...

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        maxHP = Stats.MaxHP;
        currentHP = maxHP;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHP);
    }

    void Update()
    {
        if (player == null) return;

        // Dirección hacia el jugador en el plano XZ
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        float distance = dir.magnitude;

        // Girar hacia el jugador SIEMPRE
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // Si está fuera de rango → acercarse
        if (distance > attackRange)
        {
            Vector3 moveDir = dir.normalized;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        // Si está en rango y no atacando → iniciar rutina de ataque
        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Una ráfaga con volleyCount fireballs
        for (int i = 0; i < volleyCount; i++)
        {
            if (player != null)
            {
                // Mirar al jugador antes de cada disparo
                Vector3 dir = player.position - firePoint.position;
                dir.y = 0f;
                if (dir.sqrMagnitude > 0.001f)
                    firePoint.rotation = Quaternion.LookRotation(dir);
            }

            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

            yield return new WaitForSeconds(timeBetweenFireballs);
        }

        // Siguiente vez lanza una bola más
        volleyCount++;

        // Espera hasta el siguiente ciclo de ataque
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    // Daño al boss
    public void Recibirdano(int danio)
    {
        int danioFinal = danio - Stats.Defense;
        if (danioFinal < 1) danioFinal = 1;

        currentHP -= danioFinal;

        if (healthBar != null)
            healthBar.SetHealth(currentHP);

        if (currentHP <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("🐉 Boss derrotado");
        if (GameManager.Instance != null)
            GameManager.Instance.OnBossDefeated();

        Destroy(gameObject);
    }
}
