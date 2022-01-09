using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float speed = 5f, maxspeed = 1.5f, jumpPow = 300f;
    public bool grounded = true, walk = false; 
    public Rigidbody2D r2;
    public Animator anim;
    public bool faceright = true;
    public GameObject bloodEffect;
    public float maxHealth = 30f;
    public static float curHelath;
    public GameOverScreen endGame;
    public GameObject pauseMenu;
    public GameObject informationMenu;
    public GameObject triggerAttack;
    Vector2 direction = Vector2.zero;
    public GameObject bulletPrefab;
    int bullet_quantity = 0;
    public HealthbarBehavior healthbar;
    public static float dame = 100f, defense=1f, shootdame = 150f;
    public Text quantity_bullet;
    public Text delayBullet;

    public GameObject needCollectCoin;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHelath = maxHealth;
        healthbar.SetHealthBar(curHelath, maxHealth);
        Time.timeScale = 1;
        LoadIEBullet();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetBool("walk", walk);


        if (Time.timeScale != 0) {
            if (/*CrossPlatformInputManager.GetButtonDown("Jump")*/ Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                grounded = false;
                r2.AddForce(Vector2.up * jumpPow);
            }
            if (/*CrossPlatformInputManager.GetButtonDown("Jump")*/ Input.GetKeyDown(KeyCode.P))
            {
                anim.SetTrigger("melee");
                triggerAttack.SetActive(true);
            }
            if (/*CrossPlatformInputManager.GetButtonDown("Jump")*/ Input.GetKeyUp(KeyCode.P))
            {
                triggerAttack.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (bullet_quantity > 0) {
                    anim.SetTrigger("shoot");
                    bulletPrefab.SetActive(true);
                    StartCoroutine(Shoot());
                }
                
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameManage.Instance.PauseGame();
                informationMenu.SetActive(true);
                PlayerInfoManage.Instance.UpdateInfo(this);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManage.Instance.PauseGame();
                pauseMenu.SetActive(true);
            }
        }
    }

    public float GetCurrentHealth()
    {
        return curHelath;
    }

    public void SetCurrentHealth(float health)
    {
        curHelath = health;
    }

    public float GetStrength()
    {
        return (dame/10);
    }


    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetArmor()
    {
        return defense;
    }

    public void StrengthUp()
    {
        dame += 10;
        PlayerInfoManage.Instance.UpdateInfo(this);
    }

    public void MaxHealthUp()
    {
        maxHealth += 2;
        curHelath += 2;
        PlayerInfoManage.Instance.UpdateInfo(this);
    }

    public void ArmorUp()
    {
        defense += 1;
        PlayerInfoManage.Instance.UpdateInfo(this);
    }

    private void FixedUpdate()
    {
        healthbar.SetHealthBar(curHelath, maxHealth);
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            direction = Vector2.right;
            walk = true;
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (h < 0)
        {
            direction = Vector2.left;
            walk = true;
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else walk = false;
        if (h > 0 && !faceright)
        {
            Flip();
        }
        if (h < 0 && faceright)
        {
            Flip();
        }
    }

    public void Flip()
    {
        faceright = !faceright;
        Vector3 scale;
        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "Trap")
        {
            ChangeHealth(-2000);
        }
        if (collision.gameObject.tag == "Deadline")
        {
            curHelath = 0;
            ChangeHealth(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            ScoreManager.Instance.ChangeScore(1);
        }
        if (collision.gameObject.tag == "ChangeScene")
        {
            if (ScoreManager.Instance.coinInMap < ScoreManager.Instance.totalscoreInMap)
            {
                needCollectCoin.SetActive(true);
            } else
            {
                GameManage.Instance.ChangeScene();
                ScoreManager.Instance.coinInMap = 0;


            }
        }
    }

    IEnumerator WaitBeforeDelay()
    {
        yield return new WaitForSeconds(0.7f);
        anim.gameObject.SetActive(false);
        endGame.Setup();
    }
    
    public void ChangeHealth (float health)
    {
        if (health < 0) health += defense;
        if (health >= 0) health = -1;
        curHelath += health;
        if (curHelath <= 0)
        {
            anim.SetBool("dead", true);
            Instantiate(bloodEffect, transform.position, transform.rotation);
            StartCoroutine(WaitBeforeDelay());
        }
    }

    public IEnumerator Shoot() {
        yield return new WaitForSeconds(0.35f);
        GameObject bulletObject = Instantiate(bulletPrefab, r2.position + Vector2.up * 0.1f, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.shoot(direction, 400);
        bullet_quantity -= 1;
    }

    public IEnumerator LoadBullet() {
        float timedelay = 6f;
        float time = timedelay;
        while (true) {
            if (bullet_quantity < 10 && time <= 0) {
                bullet_quantity += 1;
                time = timedelay;
            }
            quantity_bullet.text = "x " + bullet_quantity;
            yield return new WaitForSeconds(1);
            delayBullet.text = time - 1f + "s";
            time -= 1f;
        }
    }

    public void LoadIEBullet()
    {

        StartCoroutine(LoadBullet());
    }
}
