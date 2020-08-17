using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public LayerMask _enemies;
    // Start is called before the first frame update
    private ParticleSystem fireParticles;
    private List<ParticleCollisionEvent> _collisionEvents;
    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
        fireParticles.Stop();
        _collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireParticles.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            fireParticles.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("colision");
        if (other.gameObject.layer.Equals(_enemies))
        {
            Debug.Log("colision");
        }
    }
}
