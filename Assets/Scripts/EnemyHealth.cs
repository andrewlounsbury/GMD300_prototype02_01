using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour
{
    //calls for player sword game object to recieve collider, public variables for enemy health 
    public GameObject PlayerSword;
    public float EnemHealth;
    public float MaxEnemHealth = 5.0f; 
    
    //serialized private image to be set in inspector but not called by any other script, used as the fill for the enemy health bars
    [SerializeField] private Image fill; 

    //subtracts one health from the enemies current health andcalls update health bar function 
    public void TakeDamage(float damage = 1.0f)
    {
        EnemHealth -= damage;
        UpdateHealthBar();
    }

    //changes the fill amount of the health bar used for the enemy prefab
    public void UpdateHealthBar()
    {
        fill.fillAmount = EnemHealth / MaxEnemHealth;
    }

    //if enemy health is less than or equal to zero at any point per frame, the object destroys 
    void Update()
    {
        if (EnemHealth <= 0)
        { 
            Destroy(gameObject); 
        }
    }

    
}
