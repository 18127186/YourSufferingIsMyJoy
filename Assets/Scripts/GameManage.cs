using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class GameManage
{
    private static GameManage _instance;

    public bool isNewGame = true; 
    public bool isBinaryOpton = true; 

    public static GameManage Instance
    {
        get
        {
            if (_instance == null) 
            {
                _instance = new GameManage();
            }
            
            return _instance;
        }
    }

    public bool IsPause()
    {
        if(Time.timeScale == 0)
        {
            return true;
        } 
        else {
            return false;
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Pause");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void ChangeScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SaveJsonGame(GameObject player, int index) 
    {
        string path = Path.Combine(Application.persistentDataPath, "player" + index + ".hd");

        Player playerController = player.GetComponent<Player>();
        float playerHealth = playerController.GetCurrentHealth();
        Vector2 playerPosition = playerController.transform.position;
        SaveData saveData = new SaveData(playerHealth, playerPosition);

        string jsonString = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, jsonString);

        Debug.Log("Json saved " + path);
    }

    public void LoadJsonGame(Player playerController, int index)
    {
        string path = Path.Combine(Application.persistentDataPath, "player" + index + ".hd");

        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(fileContents);
            playerController.SetCurrentHealth(saveData.playerHealth);
            playerController.transform.position = new Vector2(saveData.playerPosition[0], saveData.playerPosition[1]);

            Debug.Log("Json file loaded");
        }
    }
}
