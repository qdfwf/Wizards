using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour
{
    public int damagePerSecond = 1;
    public float duration = 5f;

    private Health health;
    private bool isBurning = false;
    private Coroutine burnCoroutine;

    void Start()
    {
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("На объекте нет компонента Health.");
        }
    }

    public void ApplyFireDamage()
    {
        if (!isBurning && health != null)
        {
            isBurning = true;
            burnCoroutine = StartCoroutine(Burn());
        }
    }

    public void StopFireDamage()
    {
        if (isBurning)
        {
            StopCoroutine(burnCoroutine);
            isBurning = false;
        }
    }

    private IEnumerator Burn()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            health.TakeDamage(damagePerSecond);
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
        isBurning = false;
    }
}