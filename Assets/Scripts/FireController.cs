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
    public int damage = 10;
    private float coolDownGas = 1f;
    private float coolDownActual = 0f;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        part.Stop();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void UpdateStrength(int damage, bool state)
    {
        Debug.Log("Update");
        var main = part.main;
        if (state)
        {
            main.startLifetime = new ParticleSystem.MinMaxCurve(1.5f);
            main.startSize  = new ParticleSystem.MinMaxCurve(3.5f);
        }
        else
        {
            main.startLifetime = new ParticleSystem.MinMaxCurve(1f);
            main.startSize  = new ParticleSystem.MinMaxCurve(2f);
        }
        this.damage = damage;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<PlayerStats>().gas>0)
        {
            part.Play();
            
            // RotateTrail();
        }

        if (Input.GetMouseButton(0) && coolDownActual >= coolDownGas)
        {
            GetComponentInParent<PlayerStats>().UpdateGas(-1);
            coolDownActual = 0;
        }

        coolDownActual += Time.deltaTime;
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
                other.GetComponent<EnemyStats>().ReceiveDamage(damage);
                rb.AddForce(force, ForceMode.Impulse);
                other.GetComponent<EnemyStats>().damaged = true;
            }
        }
    }
}
