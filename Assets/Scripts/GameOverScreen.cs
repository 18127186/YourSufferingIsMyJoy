using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ContinueGame()
    {
        int currentScene = GameManager.instance.GetContinueScene();
        SceneManager.LoadScene(currentScene);
    }
    public void NewGame()
    {
        GameManager.instance.SetContinueScene(1);
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
