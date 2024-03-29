using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb2d;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot(Vector2 direction, float force) {
        rb2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        EnemyGuyController e = other.collider.GetComponent<EnemyGuyController>();
        Debug.Log(e);
        if (e != null) {
            e.ChangeHealth(-Player.shootdame);
        }
        
    }
}
