using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoSingleton<ParticleSystemManager>
{

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }
    public void RunParticleSystem(Vector3 pos)
    {
        transform.position = pos;
        ps.Play();
    }
}
