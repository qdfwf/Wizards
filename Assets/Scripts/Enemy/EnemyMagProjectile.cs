using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagProjectile : MonoBehaviour
{
    public int damage = 10; // Уровень урона, который наносит снаряд

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var healthComponent = collision.GetComponent<PlayerHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }
            Destroy(gameObject); // Удаляем снаряд после нанесения урона
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Удаляем снаряд при столкновении с землей или стеной
        }
    }
}
