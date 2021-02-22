using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PoolableObject
{
    CircleCollider2D explosionCollider;

    const float BASE_RADIUS = 0.1f;
    const float INCREMENET = 0.1f;

    bool exploding = false;
    const float BASE_TIMER = 0.3f;

    void Start() {
        explosionCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate() {
        if(exploding) {
            explosionCollider.radius += INCREMENET;
        }
    }
    public void explode() {
        exploding = true;
        StartCoroutine(finish());
    }

    IEnumerator finish() {
        yield return new WaitForSeconds(BASE_TIMER);
        exploding = false;
        explosionCollider.radius = BASE_RADIUS;
        gameObject.SetActive(false);
    }
}
