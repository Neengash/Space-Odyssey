using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLogic : PoolableObject
{
    [SerializeField] ExplosionSpawner explosions;
    [SerializeField] GameObject rockModel;
    MultipleExplosion deathExplosion;
    Rigidbody2D rb2D;
    Collider2D rockCollider;
    AudioSource audioSource;
    [SerializeField] AudioClip rockDeathSound;
    ScoreManager score;

    float rotation;
    public const float
        SLOW_ROTATION = 0.1f,
        MID_ROTATION = 0.5f,
        FAST_ROTATION = 1f;

    const float FORCE_SIZE = 15;


    void Start()
    {
        checkComponentReferences();
    }

    private void Update()
    {
        if (rotation != 0) {
            transform.Rotate(new Vector3(0, rotation, 0));
        }
    }

    void checkComponentReferences()
    {
        if (deathExplosion == null) { deathExplosion = GetComponentInChildren<MultipleExplosion>(); } 
        if (rb2D == null) { rb2D = GetComponent<Rigidbody2D>(); } 
        if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
        if (rockCollider == null) { rockCollider = GetComponent<Collider2D>(); }
        if (explosions == null) { explosions = FindObjectOfType<ExplosionSpawner>(); }
        if (score == null) { score = FindObjectOfType<ScoreManager>(); }
    }

    public void getReady() {
        checkComponentReferences();
        rockModel.SetActive(true);
        rotation = 0;
        rb2D.velocity = new Vector2(0, 0);
        rockCollider.enabled = true;
    }

    public void setMovemnt(float speedX, float speedY, float rotation) {
        rb2D.velocity = new Vector2(speedX, speedY);
        this.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.SCENARIO_LIMIT) {
            this.gameObject.SetActive(false);
            return;
        }

        if (collision.gameObject.layer == Layers.PRIMARY_ATTACK) {
            score.scoreRock();
            destroyRock();
            return;
        }

        if (collision.gameObject.layer == Layers.EXPLOSION) {
            float dx = transform.position.x - collision.transform.position.x;
            float dy = transform.position.y - collision.transform.position.y;
            float mod = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
            rb2D.AddForce(new Vector2(dx / mod * FORCE_SIZE, dy / mod * FORCE_SIZE));
            rotation = (dx <= 0) ? SLOW_ROTATION : -SLOW_ROTATION;
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if  (collision.gameObject.layer == Layers.PLAYER) {
            destroyRock();
            return;
        }
    }

    private void destroyRock()
    {
        audioSource.PlayOneShot(rockDeathSound);
        deathExplosion.playExplosion();
        rockModel.SetActive(false);
        rockCollider.enabled = false;
        spawnExplosion();
        StartCoroutine(rockDeath());
    }

    IEnumerator rockDeath()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    void spawnExplosion() {
        Explosion explosion = explosions.getExplosion();
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
        explosion.explode();
    }


    // could be that it recieves forces that push it in other directions
    //      those forces are generated when enemies or rocks are destroyed
    //      or even when getting too close to enemies
    // ---- THOSE WILL BE COLLIDERS AND SOLVE THE PROBLEM AUTOMÁTICALLY
}
