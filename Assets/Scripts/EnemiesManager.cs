using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    private static EnemiesManager _instance;

    public static EnemiesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EnemiesManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    public const int maxNumberOfEnemies = 200;
    public static int currentAmountOfEnemies = 0;
    public static int currentAmountOfSpawners = 4;
    public Text enemies;
    public Text spawners;

    private void Start()
    {
        UpdateEnemies(0);
        UpdateSpawners(0);
    }

    public void UpdateEnemies(int amount)
    {
        currentAmountOfEnemies += amount;
        enemies.text = "Enemies: " + currentAmountOfEnemies;
    }
    public void UpdateSpawners(int amount)
    {
        currentAmountOfSpawners += amount;
        spawners.text = "Spawners: " + currentAmountOfSpawners;
    }
}
