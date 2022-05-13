using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthBar;
    public float healthAmount = 100;
    //used to help restart the scene function
    [System.Obsolete]
    private void Update()
    {
        if (healthAmount <= 0)
        {
            //Resets the game
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public void DealDamage(float Damage)
    {
        healthAmount -= Damage;
        HealthBar.fillAmount = healthAmount / 100;
    }
    public void Healing(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        HealthBar.fillAmount = healthAmount / 100;
    }
}
