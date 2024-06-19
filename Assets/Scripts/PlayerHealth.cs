using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Text deathMessage;

    void Start()
    {
        currentHealth = maxHealth;
        if (deathMessage != null)
        {
            deathMessage.enabled = false; // —крыть сообщение при старте игры
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        if (deathMessage != null)
        {
            deathMessage.enabled = true; // ѕоказать сообщение о смерти
        }
        // ƒополнительна€ логика смерти, например остановка управлени€ игроком
        Destroy(gameObject);
    }
}