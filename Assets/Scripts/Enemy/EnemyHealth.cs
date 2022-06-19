using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField]private float maxHealth;
    public bool invulnerable { get; set; }

    public bool isDead { get; set; }

    private float Health;

    void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float dmg, out bool killed)
    {
        Health -= dmg;
        killed = false;

        if(Health <= 0f)
        {
            killed = true;
            isDead = true;
            return;
        }
    }
}
