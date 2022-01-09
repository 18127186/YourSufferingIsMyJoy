using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance
    {
        get; private set;
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
        text = GameObject.FindGameObjectWithTag("TextCoin").GetComponent<Text>();
        text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();

    }
    public void FixedUpdate() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
            ScoreManager.Instance.totalscoreInMap = ScoreManager.Instance.totalscoreInMap1;
        else if (currentScene == 2)
            ScoreManager.Instance.totalscoreInMap = ScoreManager.Instance.totalscoreInMap2;
        else if (currentScene == 3)
            ScoreManager.Instance.totalscoreInMap = ScoreManager.Instance.totalscoreInMap3;
        else
            ScoreManager.Instance.totalscoreInMap = 0;
        text = GameObject.FindGameObjectWithTag("TextCoin").GetComponent<Text>();
        text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();
    }
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        coinInMap += coinValue;
        text.text = coinInMap.ToString() + " / " + totalscoreInMap.ToString();
    }
}
