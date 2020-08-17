using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{
    public GameObject health;

    public GameObject gas;

    public GameObject strength;

    public int maxPowerups = 4;
    public static int currentPowerups = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(GeneratePowerup),0,10f);
    }

    // Update is called once per frame
    void GeneratePowerup()
    {
        if (currentPowerups <= maxPowerups)
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            int type = Random.Range(1, 3);
            switch (type)
            {
                case 1:
                    Instantiate(health, new Vector3(x, 0.5f, z), Quaternion.Euler(20f,0,0));
                    break;
                case 2:
                    Instantiate(gas, new Vector3(x, 0.5f, z), Quaternion.Euler(20f,0,0));
                    break;
                case 3:
                    Instantiate(strength, new Vector3(x, 0.5f, z), Quaternion.Euler(20f,0,0));
                    break;
            }
            currentPowerups++;
        }
    }
}
