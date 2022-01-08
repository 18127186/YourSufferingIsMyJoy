using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    Rigidbody2D rb2d;
    EnemyGuyController enemyGuyController;
    public float dame = 20.0f;
    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fall(float force) {
        rb2d.AddForce(Vector2.down * force);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        Player e = other.collider.GetComponent<Player>();
        if (e != null) {
            e.ChangeHealth(-dame);
        }
        
    }
}
