using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    bool invulnerable { get; set; }
    bool isDead { get; set; }

    void TakeDamage(float dmg, out bool killed);
} 
