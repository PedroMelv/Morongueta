using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeeleWeapon : Weapon
{
    [Header("Attack things")]
    public float totalCooldown;
    public float perHitCooldown;
    public int maxComboHits;

    public GameObject attackBoxes;
}
