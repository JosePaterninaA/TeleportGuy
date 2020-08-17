using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject container;
    public int amount = 10;

    public float spawnRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn),0,10);
    }
    
    private void Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            if (EnemiesManager.currentAmountOfEnemies < EnemiesManager.maxNumberOfEnemies)
            {
                Vector2 xz = UnityEngine.Random.insideUnitCircle * 3;
                GameObject currentEnemy=Instantiate(enemy,new Vector3(xz.x+transform.position.x,0.5f,xz.y+transform.position.z), Quaternion.identity);
                currentEnemy.transform.parent = container.transform;
                EnemiesManager.Instance.UpdateEnemies(1);
                // Debug.Log(EnemiesManager.Instance.enemies.text);
            }
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 60 * Time.deltaTime);
    }
}
