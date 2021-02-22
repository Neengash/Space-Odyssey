using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    ObjectPool explosionPool;

    void Start() {
        explosionPool = GetComponent<ObjectPool>();
    }

    public Explosion getExplosion() {
        return (Explosion)explosionPool.getNext();
    }

}
