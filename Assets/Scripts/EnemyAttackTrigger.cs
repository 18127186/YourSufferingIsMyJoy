using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    public GameObject enemyGuy;
    Player playerController;
    bool attack = false;
    float timer;
    public float WaitAttackTime;
    EnemyGuyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        timer = WaitAttackTime;
        enemyController = enemyGuy.GetComponent<EnemyGuyController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attack) { 
            timer -= Time.deltaTime;
            if ( timer <= 0)
            {
                timer = WaitAttackTime;
           
                playerController.ChangeHealth(-enemyController.dame);
                enemyController.Attack();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack = true;
            playerController = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timer = WaitAttackTime;
            attack = false;
            enemyController.UnAttack();
        }
    }
}
