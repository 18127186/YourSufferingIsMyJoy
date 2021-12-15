using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
            EnemyGuyController enemyController = collision.GetComponent<EnemyGuyController>();
            enemyController.ChangeHealth(-Player.dame);
        }
    }
}
