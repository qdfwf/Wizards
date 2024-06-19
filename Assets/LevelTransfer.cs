using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransfer : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // Имя следующего уровня

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкновение произошло с игроком
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
