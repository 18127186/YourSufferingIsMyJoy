using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoManage : MonoBehaviour
{
    public static PlayerInfoManage _instance;

    public static PlayerInfoManage Instance
    {
        get
        {
            if (_instance == null) 
            {
                _instance = new PlayerInfoManage();
            }
            
            return _instance;
        }
    }

    TextMeshProUGUI scoreValue;
    TextMeshProUGUI strengthValue;
    TextMeshProUGUI healthValue;
    TextMeshProUGUI armorValue;
    public Player player;

    public void Start() 
    {

    }

    public void UpdateInfo(Player playerController)
    {
        player = playerController;
        scoreValue = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
        strengthValue = GameObject.Find("StrValue").GetComponent<TextMeshProUGUI>();
        healthValue = GameObject.Find("HealthValue").GetComponent<TextMeshProUGUI>();
        armorValue = GameObject.Find("ArmorValue").GetComponent<TextMeshProUGUI>();

        scoreValue.text = ScoreManager.Instance.score.ToString();
        strengthValue.text = playerController.GetStrength().ToString(); 
        healthValue.text = playerController.GetCurrentHealth().ToString() + "/" + playerController.GetMaxHealth().ToString();
        armorValue.text = playerController.GetArmor().ToString();
    }
}
