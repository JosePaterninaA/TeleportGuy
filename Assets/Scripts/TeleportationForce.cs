using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationForce : MonoBehaviour
{
    public float radius = 10.0F;
    public float inPower = 30.0F;
    public float outPower = 30.0F;
    public LayerMask enemiesLayer;
    
    public void Pull()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius,enemiesLayer);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Vector3 forceDirection = rb.transform.position - gameObject.transform.position;
            float distance = Vector3.Magnitude(forceDirection);
            rb.AddForce(-forceDirection.normalized * inPower * 1 / distance, ForceMode.Impulse);
            // hit.GetComponent<EnemyStats>().ReceiveDamage(1);
        }
    }
    
    public void Push()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius,enemiesLayer);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Vector3 forceDirection = rb.transform.position - gameObject.transform.position;
            float distance = Vector3.Magnitude(forceDirection);
            rb.AddForce(forceDirection.normalized * outPower * 1 / distance, ForceMode.Impulse);
            // hit.GetComponent<EnemyStats>().ReceiveDamage(1);
        }
    }
}
