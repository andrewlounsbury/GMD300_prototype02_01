using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealthSystem : MonoBehaviour
{
    //public variables & objects to be set in inspector  
    public int Health = 5;
    public string SceneName;
    public Collider SpikeTrap;
    public Collider DeathPit; 
    public Slider HealthSlider;
    public int MaxHealth = 5;
    public Image HealthFill;
    
    //calls gradient asset 
    public Gradient HealthGradient; 

    void Start()
    {
        //calls set max health function on start 
        SetMaxHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        //on trigger enter with spike collider player health decreases by 1
        if (other.CompareTag("SpikeTrap"))
        {
            Health--;
            SetHealth(1); 
        }
        
        //on trigger enter with the death pit, player immediately dies 
        if (other.CompareTag("DeathPit"))
        {
            Health = 0;
        }
    }

    void Update()
    {
        //turns on cursor and switches scene to loss scene if health is equal to or less than zero 
        if (Health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneName); 
        }
    }

    public void SetMaxHealth()
    {
        //Sets slider value to the max health value on start and sets the color of the gradient of the slider fill based on health value
        HealthSlider.maxValue = MaxHealth;
        HealthFill.color = HealthGradient.Evaluate(1.0f);
    }

    public void SetHealth(int damage)
    {
        //sets health based on damage int intake, sets slider value to current health, changes the gradient accordingly 
        Health = Health - damage;
        HealthSlider.value = Health;
        HealthFill.color = HealthGradient.Evaluate(HealthSlider.normalizedValue);
    }
}
