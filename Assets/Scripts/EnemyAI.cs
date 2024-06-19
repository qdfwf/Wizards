using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 2.0f;
    [SerializeField] private float chaseRadius = 5.0f;
    [SerializeField] private float waterballSlowdownFactor = 0.5f;
    [SerializeField] private float slowdownDuration = 2.0f;
    [SerializeField] private float iceSlowdownFactor = 0.7f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector2 movement;
    private EnemyStatus enemyStatus;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyStatus = GetComponent<EnemyStatus>();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (isDead) return;

        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            movement = direction;
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 direction)
    {
        float speed = defaultSpeed;
        if (enemyStatus.currentState == EnemyState.Wet)
        {
            speed *= waterballSlowdownFactor;
        }
        else if (enemyStatus.currentState == EnemyState.Frozen)
        {
            speed *= iceSlowdownFactor;
        }
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void UpdateState(EnemyState newState)
    {
        if (newState == EnemyState.Dead)
        {
            if (!isDead)
            {
                isDead = true;
                animator.SetTrigger("Die");
                rb.velocity = Vector2.zero; // Остановите движение
            }
        }
        else
        {
            switch (newState)
            {
                case EnemyState.Wet:
                    // Обработка состояния "облит водой"
                    break;
                case EnemyState.Frozen:
                    // Обработка состояния "заморожен"
                    break;
                case EnemyState.Burning:
                    // Обработка состояния "горит"
                    break;
                default:
                    // Обработка нормального состояния
                    break;
            }
        }
    }

    public void Die()
    {
        UpdateState(EnemyState.Dead);
    }

    // Этот метод будет вызываться в конце анимации смерти
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
