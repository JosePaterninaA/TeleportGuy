using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Material damageMaterial;
    // Start is called before the first frame update
    public int health=50;
    private Material actualMaterial;
    private Renderer _renderer;
    public GameObject explodeVfx;
    private GameObject explodeVfxInstance;
    public GameObject burnVfx;
    private bool dead = false;
    public bool damaged = false;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (!_renderer)
        {
            _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        actualMaterial = _renderer.material;
        InvokeRepeating(nameof(checkDamaged),0,0.2f);
    }
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            if(!dead){
                dead = true;
                StartCoroutine(nameof(Die));
            }
        }
        else
        {
            if (!damaged)
            {
                damaged = true;
                StartCoroutine(nameof(DamageEffect));
            }
        }
    }

    IEnumerator Die()
    {
        
        _renderer.material = damageMaterial;
        yield return new WaitForSeconds(0.4f);
        explodeVfxInstance=Instantiate(explodeVfx, transform.position, Quaternion.identity);
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<CapsuleCollider>());
        _renderer.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(explodeVfxInstance);
        if (gameObject.tag.Equals("Spawner"))
        {
            EnemiesManager.Instance.UpdateSpawners(-1);

        }
        else
        {
            EnemiesManager.Instance.UpdateEnemies(-1);

        }
        Destroy(gameObject);

   
    }

    IEnumerator DamageEffect()
    {
        _renderer.material = damageMaterial;
        GameObject burnVfxInstance = null;
        if (!GetComponentInChildren<ParticleSystem>())
        {
            burnVfxInstance=Instantiate(burnVfx, transform.position, Quaternion.identity);
            burnVfxInstance.transform.parent = transform;
        }
        yield return new WaitForSeconds(0.5f);
        _renderer.material = actualMaterial;
        Destroy(burnVfxInstance);
        damaged = false;
    }



    private void Update()
    {
    }
    
    void checkDamaged()
    {
        damaged = false;
    }
}
