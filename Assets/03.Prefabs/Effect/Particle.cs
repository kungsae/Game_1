using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule endAction = particle.main;
        endAction.stopAction = ParticleSystemStopAction.Callback;
    }
    void OnParticleSystemStopped()
    {
        PoolManager<ParticleSystem>.instance.SetPool(particle);
    }
}