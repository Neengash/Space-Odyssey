using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttack : MonoBehaviour
{
    public const int STARTING_ATTACKS = 3;
    int remaining = 0;

    AudioSource audioSource;
    [SerializeField] AudioClip attackSound;
    [SerializeField] ObjectPool secondaryAttackPool;

    PlayerManager player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (!player.isDead()) {
            if (remaining > 0 && Input.GetKeyDown(KeyCode.S)) {
                launchAttack();
            }
        }
    }

    void launchAttack()
    {
        remaining--;
        audioSource.PlayOneShot(attackSound);

        SecondaryAttack attack = (SecondaryAttack)secondaryAttackPool.getNext();
        attack.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
            );
        attack.gameObject.SetActive(true);
        attack.playParticles();
    }

    public void setAttacksAvailable(int value) {
        remaining = value;
    }

    public void incrementAttacksAvailable() {
        remaining++;
    }

    public int getRemainingAttacks() {
        return remaining;
    }
}
