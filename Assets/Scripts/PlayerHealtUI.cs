using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthSlider; // Ссылка на слайдер полоски здоровья
    public PlayerHealth playerHealth; // Ссылка на компонент здоровья игрока

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not assigned.");
            return;
        }

        // Инициализировать значение слайдера
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.currentHealth;
    }

    void Update()
    {
        // Обновлять значение слайдера каждый кадр
        if (playerHealth != null)
        {
            healthSlider.value = playerHealth.currentHealth;
        }
    }
}
