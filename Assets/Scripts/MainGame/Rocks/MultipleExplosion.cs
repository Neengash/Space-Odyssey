using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleExplosion : MonoBehaviour
{
    ParticleSystem[] particles;

    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void playExplosion() {
        foreach (ParticleSystem system in particles) {
            system.Play();
        }
    }
}
