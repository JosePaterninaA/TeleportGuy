using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int gas = 100;
    public Text healthText;
    public Text gasText;
    public int powerupLayer = 10;

    private void Start()
    {
        UpdateHealth(0);
        UpdateGas(0);
    }

    public void UpdateHealth(int amount)
    {
        health += amount;
        healthText.text = "Health: " + health;
    }
    public void UpdateGas(int amount)
    {
        gas += amount;
        gasText.text = "Gas: " + gas;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer==powerupLayer)
        {
            switch (other.gameObject.tag)
            {
                case "HealthPowerUp":
                    if (health <= 80)
                    {
                        UpdateHealth(20);
                    }
                    else
                    {
                        UpdateHealth(100-health);
                    }
                    break;
                case "GasPowerUp":
                    if (gas <= 80)
                    {
                        UpdateGas(20);
                    }
                    else
                    {
                        UpdateGas(100-gas);
                    }

                    break;
            }
            
            Destroy(other.gameObject);
            PowerupsManager.currentPowerups--;
        }
    }
}
