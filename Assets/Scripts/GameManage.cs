using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManage
{
    private static GameManage _instance;

    public bool isLoadGame = false; 
    public int fileNumber; 
    public SaveData saveData;

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

        int scene = SceneManager.GetActiveScene().buildIndex;
        int playerScore = ScoreManager.Instance.score;

        Player playerController = player.GetComponent<Player>();
        Vector2 playerPosition = playerController.transform.position;
        float playerHealth = playerController.GetCurrentHealth();
        float playerMaxHealth = playerController.GetMaxHealth();
        float playerStrength = playerController.GetStrength();
        float playerArmor = playerController.GetArmor();

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        Vector2[] coinsPosition = new Vector2[coins.Length];
        for (int i = 0; i < coins.Length; i++)
        {
            coinsPosition[i] = coins[i].transform.position;
        }

        SaveData saveData = new SaveData(scene, playerScore, playerPosition, playerHealth, playerMaxHealth, playerStrength, playerArmor, coinsPosition.Length, coinsPosition);

        string jsonString = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, jsonString);

        Debug.Log("Json saved " + path);
    }

    public void LoadJsonGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "player" + fileNumber + ".hd");

        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);

            saveData = JsonUtility.FromJson<SaveData>(fileContents);

            SceneManager.LoadScene(saveData.scene);
        }

    }
}
