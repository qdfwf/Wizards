using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    private EnemyAI enemyAI;
    private Animator animator;  // Добавляем переменную для Animator

    void Start()
    {
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();  // Получаем компонент Animator
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (enemyAI != null)
            {
                animator.SetBool("isDead", true);  // Устанавливаем параметр isDead в true
                Destroy(gameObject, 1f);  // Уничтожаем объект через 1 секунду после смерти
            }
        }
    }
}
