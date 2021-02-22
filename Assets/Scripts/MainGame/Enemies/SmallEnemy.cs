using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : PoolableObject
{
    ScoreManager score;
    ExplosionSpawner explosions;
    AudioSource audioSource;
    MultipleExplosion deathExplosion;
    Collider2D enemyCollider;
    Rigidbody2D rBody2D;
    [SerializeField] GameObject enemyModel;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject engineParticles;

    float speedX;
    const float BASE_SPEED_X = -5f;

    void Start() {
        checkComponentReferences();
        speedX = BASE_SPEED_X;
    }

    void FixedUpdate() {
        rBody2D.velocity = new Vector2(speedX, 0);
    }

    void checkComponentReferences() {
        if (deathExplosion == null) { deathExplosion = GetComponentInChildren<MultipleExplosion>(); } 
        if (enemyCollider == null) { enemyCollider = GetComponent<Collider2D>(); } 
        if (rBody2D == null) { rBody2D = GetComponent<Rigidbody2D>(); } 
        if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
        if (explosions == null) { explosions = FindObjectOfType<ExplosionSpawner>(); }
        if (score == null) { score = FindObjectOfType<ScoreManager>(); }
    }

    public void getReady()
    {
        checkComponentReferences();
        enemyModel.SetActive(true);
        engineParticles.SetActive(true);
        enemyCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.SCENARIO_LIMIT) {
            gameObject.SetActive(false);
            return;
        }

        if (
            (collision.gameObject.layer == Layers.PRIMARY_ATTACK) ||
            (collision.gameObject.layer == Layers.SECONDARY_ATTACK)
        ) {
            score.scoreEnemy();
            destroyEnemy();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER) {
            destroyEnemy();
            return;
        }
    }

    private void destroyEnemy() {
        audioSource.PlayOneShot(deathSound);
        deathExplosion.playExplosion();
        enemyModel.SetActive(false);
        engineParticles.SetActive(false);
        enemyCollider.enabled = false;
        spawnExplosion();
        StartCoroutine(enemyDeath());
    }

    IEnumerator enemyDeath() {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void spawnExplosion() {
        Explosion explosion = explosions.getExplosion();
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
        explosion.explode();
    }
}
