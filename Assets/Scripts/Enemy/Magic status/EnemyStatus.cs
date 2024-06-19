using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Normal;
    private EnemyAI enemyAI;
    private Health enemyHealth;
    private FireDamage fireDamage;


    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyHealth = GetComponent<Health>();
        fireDamage = GetComponent<FireDamage>(); // Получаем компонент FireDamage
    }

    void Update()
    {
        // Проверяем текущий статус противника и применяем соответствующий эффект
        switch (currentState)
        {
            case EnemyState.Wet:
                // Применяем эффект замедления от воды...
                break;
            case EnemyState.Burning:
                fireDamage.ApplyFireDamage(); // Применяем эффект горения
                // Остальные действия при горении...
                break;
            case EnemyState.Frozen:
                // Применяем эффект заморозки...
                break;
            case EnemyState.Normal:
                // Возвращаемся к обычному состоянию...
                break;
            default:
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
        enemyAI.UpdateState(newState);
    }

    public void ApplyFireDamage()
    {
        if (currentState == EnemyState.Burning)
        {
            fireDamage.ApplyFireDamage(); // Вызываем метод из FireDamage
        }


        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Normal);
        }
        else if (currentState == EnemyState.Frozen)
        {
            ChangeState(EnemyState.Wet);
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Burning);
        }

        // Можем добавить другую логику при необходимости
    }

    public void ApplyIceDamage()
    {
        if (currentState == EnemyState.Wet)
        {
            ChangeState(EnemyState.Frozen);
        }
        else if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet);
        }
        else if (currentState == EnemyState.Burning)
        {
            ChangeState(EnemyState.Wet);
        }
    }

    public void ApplyShockDamage()
    {
        if (currentState == EnemyState.Frozen)
        {
            enemyHealth.TakeDamage(1000);

            Debug.Log("Враг получил шоковый урон в состоянии заморозки!");
        }
        else if (currentState == EnemyState.Wet)
        {
            enemyHealth.TakeDamage(50);

            Debug.Log("Враг получил шоковый урон в состоянии мокрости!");
        }
        else
        {
            enemyHealth.TakeDamage(25);

            Debug.Log("Враг получил шоковый урон");
        }
    }

    public void ApplyWaterEffect()
    {
        if (currentState == EnemyState.Normal)
        {
            ChangeState(EnemyState.Wet);
        }
        // Можем добавить другую логику при необходимости
        else if (currentState == EnemyState.Burning)
        {
            // Логика для шока во время облития водой, например, увеличение урона
            ChangeState(EnemyState.Normal);
        }
    }
}