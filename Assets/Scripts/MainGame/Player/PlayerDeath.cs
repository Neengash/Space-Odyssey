using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField] ParticleSystem[] particles;

    public void playDeath()
    {
        foreach (ParticleSystem particle in particles) {
            particle.Play();
        }
    }
}
