using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float pushForce = 2f;
    // Start is called before the first frame update
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float trailRotationSpeed=20f;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        part.Stop();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<PlayerStats>().gas>0)
        {
            part.Play();
            GetComponentInParent<PlayerStats>().UpdateGas(-1);
            // RotateTrail();
        }

        if (Input.GetMouseButtonUp(0))
        {
            part.Stop();
        }
        
    }




    void OnParticleCollision(GameObject other)
    {
        if (other)
        {
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
    
            Rigidbody rb = other.GetComponent<Rigidbody>();
            int i = 0;
            if (rb && !other.GetComponent<EnemyStats>().damaged && numCollisionEvents>=1)
            {
                // Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity.normalized*pushForce;
                other.GetComponent<EnemyStats>().ReceiveDamage(10);
                rb.AddForce(force, ForceMode.Impulse);
                other.GetComponent<EnemyStats>().damaged = true;
            }
        }
    }
}
