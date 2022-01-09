using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

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
    public GameObject pauseBtn;
    public GameObject infoBtn;
    public GameObject skillUI;
    public GameObject triggerAttack;
    public GameObject coinObj;
    Vector2 direction = Vector2.zero;
    public GameObject bulletPrefab;
    int bullet_quantity = 0;
    public HealthbarBehavior healthbar;
    public static float dame = 100f, defense=1f, shootdame=150;
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

        if (GameManage.Instance.isLoadGame)
        {   
            SaveData saveData = GameManage.Instance.saveData;

            if (saveData != null) {
                curHelath = saveData.playerHealth;
                maxHealth = saveData.playerMaxHealth;
                dame = saveData.playerStrength*10;
                shootdame = 150 + (saveData.playerStrength*10 - 100)* 20;
                defense = saveData.playerArmor;

                healthbar.SetHealthBar(curHelath, maxHealth);

                transform.position = new Vector3(saveData.playerPosition[0], saveData.playerPosition[1]);

                GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
                for (int i = coins.Length; i > 0; i--) {
                    Destroy(coins[i - 1]);
                }

                for (int i = 0; i < saveData.numCoin; i++) {
                    Vector3 coinPosition = new Vector3(saveData.coinsXPosition[i], saveData.coinsYPosition[i], 0);
                    Instantiate(coinObj, coinPosition, Quaternion.identity);
                }

                ScoreManager.Instance.score = saveData.playerScore;
                ScoreManager.Instance.coinInMap = ScoreManager.Instance.totalscoreInMap - saveData.numCoin;

      

                ScoreManager.Instance.ChangeScore(0);

                GameManage.Instance.isLoadGame = false;
            }
        }
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
                Info();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
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
        if (ScoreManager.Instance.score > 0) {
            dame += 10;
            shootdame += 20;
            ScoreManager.Instance.ChangePlayerScore(-1);
            PlayerInfoManage.Instance.UpdateInfo(this);
        }
    }

    public void MaxHealthUp()
    {
        if (ScoreManager.Instance.score > 0) {
            maxHealth += 5;
            curHelath += 5;
            healthbar.SetHealthBar(curHelath, maxHealth);
            ScoreManager.Instance.ChangePlayerScore(-1);
            PlayerInfoManage.Instance.UpdateInfo(this);
        }
    }

    public void ArmorUp()
    {
        if (ScoreManager.Instance.score > 0) {
            defense += 1;
            ScoreManager.Instance.ChangePlayerScore(-1);
            PlayerInfoManage.Instance.UpdateInfo(this);
        }
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
                StartCoroutine(TurnFLagOff());
            } else
            {
                GameManage.Instance.ChangeScene();
                ScoreManager.Instance.coinInMap = 0;
            }
        }
    }

    IEnumerator TurnFLagOff()
    {
        yield return new WaitForSeconds(2f);
        needCollectCoin.SetActive(false);
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
        if (bullet_quantity > 0) {
            bullet_quantity -= 1;
        }
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

    public void Pause() {
        GameManage.Instance.PauseGame();
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        infoBtn.SetActive(false);
        skillUI.SetActive(false);
    }

    public void Info() {
        GameManage.Instance.PauseGame();
        informationMenu.SetActive(true);
        pauseBtn.SetActive(false);
        infoBtn.SetActive(false);
        skillUI.SetActive(false);
        PlayerInfoManage.Instance.UpdateInfo(this);
    }
}
