using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    public GameObject enemyGuy;
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
        Debug.Log(1);
        EnemyGuyController enemyController = enemyGuy.GetComponent<EnemyGuyController>();
        Player playerController = collision.GetComponent<Player>();
        if (collision.tag == "Player")
        {
            playerController.ChangeHealth(-1);
            enemyController.Attack();
        }
    }
}
