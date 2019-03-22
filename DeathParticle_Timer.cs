using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem particle;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Update(){

        if(particle)
            if(!particle.IsAlive())
                Destroy(gameObject);
    }

}
