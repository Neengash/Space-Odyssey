using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool dead;

    PlayerDeath death;
    [SerializeField] GameObject[] DeactivateOnDeath;
    Collider2D playerCollider;
    Rigidbody2D rBody2D;
    GameManager game;

    AudioSource audioSource;
    [SerializeField] AudioClip deathSound;

    void Start() {
        dead = false;
        game = FindObjectOfType<GameManager>();
        death = GetComponentInChildren<PlayerDeath>();
        playerCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        rBody2D = GetComponent<Rigidbody2D>();
    }

    public bool isDead() {
        return dead;
    }

    public void die() {
        if (!dead)
        {
            dead = true;
            deactivateDeathObjects();
            death.playDeath();
            audioSource.PlayOneShot(deathSound);
            game.gameOver();
        }
    }

    void deactivateDeathObjects()
    {
        playerCollider.enabled = false;
        rBody2D.isKinematic = true;
        foreach (GameObject element in DeactivateOnDeath) {
            element.SetActive(false);
        }
    }
}
