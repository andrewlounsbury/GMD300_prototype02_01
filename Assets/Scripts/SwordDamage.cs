using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    //on enemy enter sword collider trigger, calls enemy health script and subtracts one unit from their health value
    private void OnTriggerEnter(Collider Enemy)
    {
        var EnemyHealth = Enemy.gameObject.GetComponent<EnemyHealth>();
        
        if (EnemyHealth != null)
        {
            EnemyHealth.TakeDamage();
        }
    }
}
