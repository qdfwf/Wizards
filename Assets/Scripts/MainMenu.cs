using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Этот метод вызывается при нажатии кнопки "Play"
    public void PlayGame()
    {
        // Замените "GameScene" на имя вашей игровой сцены
        SceneManager.LoadScene("Tutorial");
    }

    // Этот метод вызывается при нажатии кнопки "Options"
    public void OpenOptions()
    {
        // Здесь вы можете открыть меню настроек
        Debug.Log("Options button clicked!");
    }

    // Этот метод вызывается при нажатии кнопки "Quit"
    public void QuitGame()
    {
        // Выход из игры
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}
