using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : MonoBehaviour
{
    [SerializeField] ObjectPool misilesPool;

    AudioSource audioSource;
    [SerializeField] AudioClip attackSound;

    PlayerManager player;

    float attackTimer;
    const float TIME_BETWEEN_ATTACKS = 0.3f;
    const float MARGIN_X = 1f;
    const float MARGIN_Y = -0.1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (!player.isDead()) {
            attackTimer -= Time.deltaTime;

            if (
                (Input.GetKeyDown(KeyCode.A)) ||
                (Input.GetKey(KeyCode.A) && attackTimer <= 0)
            )
            {
                attack();
                attackTimer = TIME_BETWEEN_ATTACKS;
            }
        }
    }

    void attack()
    {
        audioSource.PlayOneShot(attackSound);

        PoolableObject misile = misilesPool.getNext();
        misile.transform.position = new Vector3(
            transform.position.x + MARGIN_X,
            transform.position.y + MARGIN_Y,
            transform.position.z);
        misile.gameObject.SetActive(true);
    }
}
