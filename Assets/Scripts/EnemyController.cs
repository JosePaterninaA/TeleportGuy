using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    private NavMeshAgent agent;
    private Transform target;
    public float range = 10.0f;

    private const int navigating = 0;
    private const int chasing = 1;
    private const int reachedPlayer = 2;
    private const int reachedNavPoint = 3;

    private float coolDownDagame = 1f;
    private float coolDownActual = 0f;
    
    
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        state = navigating;
        agent.SetDestination(RandomTarget());
    }

    Vector3 RandomTarget()
    {
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(-20f, 20f);
        return new Vector3(x,0f,z);
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        for (int i = 0; i < 30; i++) {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        switch (state)
        {
            case navigating:
                if (distance <= lookRadius)
                {
                    agent.SetDestination(target.position);
                    agent.updatePosition = true;
                    state = chasing;
                }

                if (agent.remainingDistance < 0.1f)
                {
                    state = reachedNavPoint;
                }
                break;
            case chasing:
                if (distance > lookRadius)
                {
                    agent.SetDestination(RandomTarget());
                    state = navigating;
                }
                else
                {
                    agent.SetDestination(target.position);
                }
                if (agent.remainingDistance < 1.5f)
                {
                    state = reachedPlayer;
                }
                break;
            case reachedPlayer:
                if (coolDownActual >= coolDownDagame)
                {
                    target.GetComponent<PlayerStats>().UpdateHealth(-1);
                    coolDownActual = 0;
                }

                if (distance > lookRadius)
                {
                    state = navigating;
                }

                break;
            case reachedNavPoint:
                agent.SetDestination(RandomTarget());
                state = navigating;
                break;
        }
        coolDownActual += Time.deltaTime;
        Debug.DrawLine(transform.position,agent.destination,Color.blue);
    }
}
