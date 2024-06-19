using UnityEngine;

public class WaterBall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, попал ли шарик в противника
        FireDamage fireDamage = collision.gameObject.GetComponent<FireDamage>();
        if (fireDamage != null)
        {
            // Прекращаем урон от огня
            fireDamage.StopFireDamage();

            // Уничтожаем шарик после столкновения
            Destroy(gameObject);
        }
    }
}