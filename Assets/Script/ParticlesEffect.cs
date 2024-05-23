using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem pSystem;

    public void ActiveParticles()
    {
        pSystem.Play();
    }
}
