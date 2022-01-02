using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreManager();
            }

            return _instance;
        }
    }
    public static ScoreManager instance;
    public Text text;
    public Text map;
    public int score;
    public int totalscore;
    // Start is called before the first frame update
    void Start()
    {
        text.text = score.ToString() + " / " + totalscore.ToString();
        // int currentScene = GameManager.instance.GetContinueScene();
        // if (currentScene == 0) currentScene = 1;
        // map.text = "Level " + currentScene.ToString();

    }
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = score.ToString() + " / " + totalscore.ToString();
    }
}
