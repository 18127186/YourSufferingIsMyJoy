using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public float playerHealth;
    public float[] playerPosition;
    // int[] enemyHealth;
    // float[] enemyPosition;

    // public RubyData(int playerHealth, Vector2 playerPosition, int[] enemyHealth, Vector2[] enemyPosition)
    public SaveData(float playerHealth, Vector2 playerPosition)
    {
        this.playerHealth = playerHealth;
        this.playerPosition = new float[2];
        this.playerPosition[0] = playerPosition.x;
        this.playerPosition[1] = playerPosition.y;

        // for (int i = 0; i < enemyHealth.Length; i++) {
        //     this.enemyHealth[i];
        // }

        // for (int i = 0; i < enemyPosition.Length; i += 2) {
        //     this.enemyPosition[i];
        //     this.enemyPosition[i + 1];
        // }
    }
}
