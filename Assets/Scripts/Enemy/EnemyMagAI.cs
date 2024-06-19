using UnityEngine;
using System.Collections;

public class EnemyMagAI : MonoBehaviour
{
    public GameObject projectile;
    public float shootInterval = 2f;
    public float projectileSpeed = 5f;
    public float moveSpeed = 2f;
    public float freezeDuration = 2f;
    public float attackRange = 5f;
    private float shootTimer;
    private Transform player;
    private bool isFrozen = false;

    void Start()
    {
        // Находим игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isFrozen)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                // Движение к игроку
                MoveTowardsPlayer();
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval && distanceToPlayer <= attackRange)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Поворот противника в сторону игрока
        Vector3 scale = transform.localScale;
        if (direction.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void Shoot()
    {
        if (player == null) return;

        // Создаем снаряд
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        // Вычисляем направление к игроку
        Vector2 direction = (player.position - transform.position).normalized;

        // Устанавливаем скорость снаряда
        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        // Поворачиваем противника в сторону игрока
        Vector3 scale = transform.localScale;
        if (direction.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    public void Freeze()
    {
        Debug.Log("EnemyMagAI: Freeze called");
        if (!isFrozen)
        {
            Debug.Log("EnemyMagAI: Starting FreezeCoroutine");
            StartCoroutine(FreezeCoroutine());
        }
    }

    private IEnumerator FreezeCoroutine()
    {
        isFrozen = true;
        Debug.Log("EnemyMagAI: Frozen");
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("EnemyMagAI: Unfrozen");
        isFrozen = false;
    }
}