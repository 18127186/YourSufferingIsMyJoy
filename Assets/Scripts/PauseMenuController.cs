using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        GameManage.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManage.Instance.ResumeGame();
        this.gameObject.SetActive(false);
    }

    public void SaveGame(int index)
    {
        GameManage.Instance.SaveJsonGame(player, index);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
