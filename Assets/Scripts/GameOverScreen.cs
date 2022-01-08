using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject menu;
    public GameObject guideline;
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Player playerController = player.GetComponent<Player>();
        playerController.transform.position = new Vector2(0f, 2f);
        gameObject.SetActive(false);
        player.SetActive(true);
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
    public void GuideLine()
    {
        guideline.SetActive(true);
        menu.SetActive(false);
    }
    public void CancelGuideLine ()
    {
        guideline.SetActive(false);
        menu.SetActive(true);

    }
}
