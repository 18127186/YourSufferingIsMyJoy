using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        MakeSingleInstance();
    }
    void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetContinueScene(int index)
    {
        PlayerPrefs.SetInt("CurrentScene", index);
    }
    public int GetContinueScene()
    {
        return PlayerPrefs.GetInt("CurrentScene");
    }
}
