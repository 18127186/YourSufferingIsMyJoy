using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance
    {
        get; set;
    }
    
    public Text text;
    public int score;
    public int totalscoreInMap, totalscoreInMap1, totalscoreInMap2, totalscoreInMap3;
    public int coinInMap = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
            totalscoreInMap = totalscoreInMap1;
        else if (currentScene == 2)
            totalscoreInMap = totalscoreInMap2;
        else if (currentScene == 3)
            totalscoreInMap = totalscoreInMap3;
        else {
            totalscoreInMap = 0;
        }

        GameObject textObj = GameObject.FindGameObjectWithTag("TextCoin");
        if (textObj != null) {
            text = textObj.GetComponent<Text>();
            text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();
        }
    }

    public void FixedUpdate() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
            totalscoreInMap = totalscoreInMap1;
        else if (currentScene == 2)
            totalscoreInMap = totalscoreInMap2;
        else if (currentScene == 3)
            totalscoreInMap = totalscoreInMap3;
        else {
            totalscoreInMap = 0;
        }
        GameObject textObj = GameObject.FindGameObjectWithTag("TextCoin");
        if (textObj != null) {
            text = textObj.GetComponent<Text>();
            text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();
        }
    }
    
    public void ChangeScore(int coinValue)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
            totalscoreInMap = totalscoreInMap1;
        else if (currentScene == 2)
            totalscoreInMap = totalscoreInMap2;
        else if (currentScene == 3)
            totalscoreInMap = totalscoreInMap3;
        else {
            totalscoreInMap = 0;
        }
        Debug.Log(coinInMap);
        score += coinValue;
        coinInMap += coinValue;
        GameObject textObj = GameObject.FindGameObjectWithTag("TextCoin");
        if (textObj != null) {
            text = textObj.GetComponent<Text>();
            text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();
        }   
    }

    public void ChangePlayerScore(int coinValue)
    {
        score += coinValue;
    }
}
