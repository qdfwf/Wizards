using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Параметры для стрельбы кубиками
    public GameObject[] cubePrefabs; // Массив префабов кубиков
    public float[] shootForces; // Массив сил стрельбы для каждого типа кубика
    public float[] lifeTimes; // Массив времени жизни для каждого типа кубика
    public float cooldownTime = 1f; // Время перезарядки в секундах
    public Image cooldownImage; // UI элемент для отображения перезарядки
    public float spawnOffset = 1f; // Сдвиг от позиции игрока для создания кубика
    public Sprite[] weaponIcons; // Массив иконок для каждого типа кубика
    public Image selectedWeaponIcon; // UI элемент для отображения текущего выбранного снаряда

    // Параметры для здоровья
    public int maxHealth = 100;
    public int currentHealth;
    public Text deathMessage;

    private bool facingRight = true; // Направление, куда смотрит игрок
    private int currentCubeIndex = 0; // Текущий индекс выбранного кубика
    private bool isCoolingDown = false; // Флаг перезарядки
    private bool isDead = false; // Флаг смерти
    private Animator animator; // Компонент анимации

    void Start()
    {
        currentHealth = maxHealth;
        if (deathMessage != null)
        {
            deathMessage.enabled = false; // Скрыть сообщение при старте игры
        }
        animator = GetComponent<Animator>();
        UpdateSelectedWeaponIcon();
    }

    void Update()
    {
        if (isDead)
            return;

        // Обработка поворота игрока (для примера, по горизонтальному вводу)
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        // Выпускание кубиков при нажатии мыши
        if (Input.GetMouseButtonDown(0) && !isCoolingDown)
        {
            Shoot();
        }

        // Переключение кубиков при нажатии цифр
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCubeIndex = 0;
            UpdateSelectedWeaponIcon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cubePrefabs.Length > 1)
        {
            currentCubeIndex = 1;
            UpdateSelectedWeaponIcon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && cubePrefabs.Length > 2)
        {
            currentCubeIndex = 2;
            UpdateSelectedWeaponIcon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && cubePrefabs.Length > 3)
        {
            currentCubeIndex = 3;
            UpdateSelectedWeaponIcon();
        }

        // Обновление UI состояния перезарядки
        if (isCoolingDown)
        {
            cooldownImage.fillAmount += Time.deltaTime / cooldownTime;
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Shoot()
    {
        // Определяем позицию спавна кубика
        Vector3 spawnPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * spawnOffset;

        // Создание кубика
        GameObject cubeObj = Instantiate(cubePrefabs[currentCubeIndex], spawnPosition, Quaternion.identity);

        // Установим слой кубика
        cubeObj.layer = LayerMask.NameToLayer("Projectile");

        // Получение компонента Projectile и настройка его
        Projectile projectileScript = cubeObj.GetComponent<Projectile>();
        Vector2 forceDirection = facingRight ? Vector2.right : Vector2.left;

        // Установка параметров снаряда
        projectileScript.Shoot(forceDirection, shootForces[currentCubeIndex]);
        StartCoroutine(DestroyProjectileAfterLifetime(cubeObj, lifeTimes[currentCubeIndex]));

        // Начинаем перезарядку
        StartCoroutine(Cooldown());
    }

    IEnumerator DestroyProjectileAfterLifetime(GameObject projectile, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(projectile);
    }

    IEnumerator Cooldown()
    {
        isCoolingDown = true;
        cooldownImage.fillAmount = 0;
        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = Mathf.Clamp01(elapsed / cooldownTime);
            yield return null;
        }
        isCoolingDown = false;
        cooldownImage.fillAmount = 0;
    }

    void UpdateSelectedWeaponIcon()
    {
        if (selectedWeaponIcon != null && weaponIcons.Length > currentCubeIndex)
        {
            selectedWeaponIcon.sprite = weaponIcons[currentCubeIndex];
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
        isDead = true;
        if (deathMessage != null)
        {
            deathMessage.enabled = true; // Показать сообщение о смерти
        }
        animator.SetTrigger("Die");
    }
}
