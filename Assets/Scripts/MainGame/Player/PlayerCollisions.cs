using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            (collision.gameObject.layer == Layers.ROCK) ||
            (collision.gameObject.layer == Layers.ENEMY)
        ) {
            player.die();
        }
    }
}
