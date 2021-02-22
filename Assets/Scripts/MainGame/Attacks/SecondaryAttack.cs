using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryAttack : PoolableObject
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] ParticleSystem particles;
    const float BASE_FORCE_X = 0.45f;
    const float FORCE_INCREMENT = 0.03f;
    const float BASE_TIME_ALIVE = 5f;
    float force;
    float timer;

    void Start() {
        rb2D.velocity = new Vector2(0, 0);
        force = BASE_FORCE_X;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            this.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() {
        rb2D.AddForce(new Vector2(force, 0));
        force += FORCE_INCREMENT;
    }

    public void playParticles() {
        particles.Play();
        timer = BASE_TIME_ALIVE;
    }

}
