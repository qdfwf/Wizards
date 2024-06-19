using UnityEngine;
using System.Collections;

public enum ProjectileType
{
    Waterball,
    Fireball,
    Iceball,
    Lightning
}

public class Projectile : MonoBehaviour
{
    public ProjectileType projectileType;
    public float damage;
    public float waterballSlowdownDuration = 2.0f; // Продолжительность замедления для Waterball


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                switch (projectileType)
                {
                    case ProjectileType.Waterball:
                        StartCoroutine(ApplyWaterballEffect(enemyStatus));
                        break;
                    case ProjectileType.Fireball:
                        enemyStatus.ApplyFireDamage();
                        break;
                    case ProjectileType.Iceball:
                        enemyStatus.ApplyIceDamage();
                        break;
                    case ProjectileType.Lightning:
                        enemyStatus.ApplyShockDamage();
                        break;
                }

                // Уничтожаем снаряд после столкновения
                Destroy(gameObject);
            }
        }
    }



    private IEnumerator ApplyWaterballEffect(EnemyStatus enemyStatus)
    {
        enemyStatus.ApplyWaterEffect();
        yield return new WaitForSeconds(waterballSlowdownDuration);
        if (enemyStatus.currentState == EnemyState.Wet)
        {
            enemyStatus.ChangeState(EnemyState.Normal);
        }
    }

    public void Shoot(Vector2 direction, float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
