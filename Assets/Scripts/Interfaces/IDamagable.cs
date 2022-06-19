using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int maxHealth { get; set; }

    bool invulnerable { get; set; }

    void TakeDamage(int dmg);
} 
