using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image hp;
    [SerializeField] private Health health;


    void Update()
    {
        if (health != null)
        hp.fillAmount = health.getCurrentHealth() / health.getMaxHealth();
    }
}
