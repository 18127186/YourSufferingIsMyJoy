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
    public float maxHealth = 1;
    float curHealth;
    public float dame, defense;
    public GameObject coin;
    public GameObject meteoritePrefab;
    public HealthbarBehavior healthbar;
    // Start is called before the first frame update
    void Start()
    {
        dame = 10f;
        defense = 5f;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timer = changeDirectionTime;
        curHealth = maxHealth;
        healthbar.SetHealthBar(curHealth, maxHealth);

        if (gameObject.tag == "boss") {
            StartCoroutine(initMeteorite());
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthbar.SetHealthBar(curHealth, maxHealth);
        if (curHealth <= 0)
        {
            //anim.SetBool("died", true);
            died =true;
            StartCoroutine(WaitBeforeDelay());
        }
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
    public void UnAttack()
    {
        walk = true;
        attack = false;
    }
    public void ChangeHealth(float health)
    {
        if (health < 0) health += defense;
        if (health >= 0) health = -1;
        curHealth += health;
    }
    IEnumerator WaitBeforeDelay()
    {
        int randomValue = Random.Range(0, 10);
        yield return new WaitForSeconds(0.65f);
        if (randomValue >= 7) Instantiate(coin, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public IEnumerator initMeteorite() {
        while (true) {
            int x = Random.Range(-15, 27);
            Vector2 pos = new Vector2(x, 1.6f);
            GameObject meteoriteObject = Instantiate(meteoritePrefab, pos, Quaternion.identity);
            Meteorite meteorite = meteoriteObject.GetComponent<Meteorite>();
            meteorite.fall(300f);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
