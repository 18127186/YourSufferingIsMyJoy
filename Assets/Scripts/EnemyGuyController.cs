using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuyController : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody2D rb2d;
    float timer;
    Animator anim;
    private int direction = 1;
    public float changeDirectionTime = 3f;
    bool walk = true, attack = false, died = false;
    public int maxHealth = 5;
    int curHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timer = changeDirectionTime;
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 0)
        {
            transform.rotation *= Quaternion.Euler(0, -180f, 0);
            direction *= -1;
            timer = changeDirectionTime;
        }

        anim.SetBool("walk", walk);

        anim.SetBool("attack", attack);
        anim.SetBool("died", died);
        if (walk)
        {
            timer -= Time.deltaTime;
            Vector2 position = transform.position;
            position.x += (speed * direction * Time.deltaTime);
            rb2d.MovePosition(position);
        }
    }
    public void Attack ()
    {
        walk = false;
        attack = true;
    }
}
