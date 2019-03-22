using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class particleAttractorLinear : MonoBehaviour {
	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;

    public bool check = true;

    
	public float speed;
	int numParticlesAlive;

    void Start () {

        ps = GetComponent<ParticleSystem>();
		if (!GetComponent<Transform>()){
			GetComponent<Transform>();
		}
	}
	void Update () {
		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
		numParticlesAlive = ps.GetParticles(m_Particles);
		float step = speed * Time.deltaTime;
        
        for (int i = 0; i < numParticlesAlive; i++)
        {
            Debug.Log("Hitting target!");
            m_Particles[i].position = Vector3.LerpUnclamped(m_Particles[i].position, GameObject.FindGameObjectWithTag("Enemy").transform.position, step); //Sends every particle to enemy
        }
        ps.SetParticles(m_Particles, numParticlesAlive);

    }
}
