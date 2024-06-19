using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private GameObject damageSpritePrefab; // Префаб спрайта урона
    [SerializeField] private float damageDuration = 0.5f; // Продолжительность отображения урона

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем столкновение с игровыми объектами, которые могут наносить урон
        if (collision.gameObject.CompareTag("Thunder"))
        {
            // Создаем экземпляр префаба спрайта урона
            GameObject damageSprite = Instantiate(damageSpritePrefab, transform.position + Vector3.up, Quaternion.identity);

            // Уничтожаем спрайт урона через damageDuration секунд
            Destroy(damageSprite, damageDuration);
        }
    }
}
