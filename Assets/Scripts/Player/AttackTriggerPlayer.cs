using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerPlayer : MonoBehaviour
{
    bool attack = false;
    float timer;
    public float WaitAttackTime = 0.675f;
    EnemyGuyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        timer = WaitAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = WaitAttackTime;
                enemyController.ChangeHealth(-Player.dame);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.tag == "AttackFromEnemy")
        {
            attack = true;
            enemyController = collision.GetComponentInParent<EnemyGuyController>();
            enemyController.ChangeHealth(-Player.dame);
        }
        if (collision.tag == "enemy" || collision.tag =="boss")
        {

            attack = true;
            enemyController = collision.GetComponent<EnemyGuyController>();
            enemyController.ChangeHealth(-Player.dame);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy" || collision.tag == "boss")
        {
            timer = WaitAttackTime;
            attack = false;
        }
    }
}
