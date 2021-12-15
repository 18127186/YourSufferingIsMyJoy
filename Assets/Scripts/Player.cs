using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    public float speed = 5f, maxspeed = 1.5f, jumpPow = 300f;
    public bool grounded = true, walk = false; 
    public Rigidbody2D r2;
    public Animator anim;
    public bool faceright = true;
    public GameObject bloodEffect;
    public int maxHealth = 3;
    public static int curHelath;
    public GameOverScreen endGame;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHelath = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetBool("walk", walk);
        if (/*CrossPlatformInputManager.GetButtonDown("Jump")*/ Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            r2.AddForce(Vector2.up * jumpPow);
         
        }
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            walk = true;
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (h < 0)
        {
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
            anim.SetBool("dead", true);
            Instantiate(bloodEffect, transform.position, transform.rotation);
            StartCoroutine(WaitBeforeDelay());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            ScoreManager.instance.ChangeScore(1);
        }
    }
    IEnumerator WaitBeforeDelay()
    {
        yield return new WaitForSeconds(0.7f);
        anim.gameObject.SetActive(false);
        endGame.Setup();
    }
    public void ChangeHealth (int health)
    {
        curHelath += health;
        Debug.Log(curHelath);
    }
}
