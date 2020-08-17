using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Teleportation : MonoBehaviour
{
    private bool teleportingOut = false;
    private bool teleportingIn = false;
    private TeleportationForce force;

    // Start is called before the first frame update
    public float teleportSpeed = 2f;
    public float maxShrink = 2f;
    public float teleportDelay = 0.5f;
    // public float teleportTime = 3.0f;
    public float minWait = 3.0f;
    public float maxWait = 5.0f;
    private bool moving = false;
    public GameObject player;
    void Start()
    {
        force = GetComponent<TeleportationForce>();
        // InvokeRepeating(nameof(Teleport),2.0f,teleportTime);
        StartCoroutine(nameof(TeleportLoop));

    }

    // Update is called once per frame
    void Update()
    {
        if (teleportingOut)
        {
            teleportingIn = false;
            Vector3 size = transform.localScale;
            Vector3 newSize = size / maxShrink;
            transform.localScale = Vector3.Lerp(size, newSize, teleportSpeed);
            if (size.magnitude<0.1f)
            {
                GetComponentInChildren<ParticleSystem>().Stop();
                force.Pull();
                player.GetComponent<SkinnedMeshRenderer>().enabled = false;
                teleportingOut = false;
                
                StartCoroutine("TeleportDelayPass");
            }
        }
        if (teleportingIn)
        {
            Vector3 size = transform.localScale;
            Vector3 newSize = new Vector3(10,10,10);
            transform.localScale = Vector3.Lerp(size, newSize, teleportSpeed);
        }
    }
    
    IEnumerator TeleportDelayPass()
    {
        yield return new WaitForSeconds(teleportDelay);
        teleportingIn = true;
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(-20f, 20f);
        gameObject.transform.position = new Vector3(x, 0.5f, z);
        player.GetComponent<SkinnedMeshRenderer>().enabled = true;
        force.Push();

    }

    void Teleport()
    {
        teleportingOut = true;
    }
    
    IEnumerator TeleportLoop()
    { 
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            Teleport();
        }
    }
}