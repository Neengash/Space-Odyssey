using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : PoolableObject
{
    Rigidbody2D rb2D;
    float speedX;
    const float BASE_SPEED = 8f;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        speedX = BASE_SPEED;
    }

    private void FixedUpdate() {
        rb2D.velocity = new Vector2(speedX, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (
            (collision.gameObject.layer == Layers.SCENARIO_LIMIT) ||
            (collision.gameObject.layer == Layers.ROCK) ||
            (collision.gameObject.layer == Layers.ENEMY)
        ) {
            this.gameObject.SetActive(false);
        }
    }
}
