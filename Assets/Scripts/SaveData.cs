using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int scene;
    public int playerScore;

    public float[] playerPosition;
    public float playerHealth;
    public float playerMaxHealth;
    public float playerStrength;
    public float playerArmor;

    public int numCoin;
    public float[] coinsXPosition;
    public float[] coinsYPosition;

    // int[] enemyHealth;
    // float[] enemyPosition;

    // public RubyData(int playerHealth, Vector2 playerPosition, int[] enemyHealth, Vector2[] enemyPosition)
    public SaveData(int scene, int playerScore, Vector2 playerPosition, float playerHealth, float playerMaxHealth, float playerStrength, float playerArmor, int numCoin, Vector2[] coinsPosition)
    {
        this.scene = scene;
        this.playerScore = playerScore;

        this.playerPosition = new float[2];
        this.playerPosition[0] = playerPosition.x;
        this.playerPosition[1] = playerPosition.y;
        this.playerHealth = playerHealth;
        this.playerMaxHealth = playerMaxHealth;
        this.playerStrength = playerStrength;
        this.playerArmor = playerArmor;

        this.numCoin = numCoin;
        this.coinsXPosition = new float[numCoin];
        this.coinsYPosition = new float[numCoin];
        for(int i = 0; i < numCoin; i++)
        {
            this.coinsXPosition[i] = coinsPosition[i].x;
            this.coinsYPosition[i] = coinsPosition[i].y;
        }

        // for (int i = 0; i < enemyHealth.Length; i++) {
        //     this.enemyHealth[i];
        // }

        // for (int i = 0; i < enemyPosition.Length; i += 2) {
        //     this.enemyPosition[i];
        //     this.enemyPosition[i + 1];
        // }
    }
}
