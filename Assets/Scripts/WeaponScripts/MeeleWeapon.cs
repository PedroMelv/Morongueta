using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeeleWeapon : Weapon
{
    [Header("Attack things")]
    public float totalCooldown;
    public float perHitCooldown;
    public float attackDuration;
    public int maxComboHits;

    public AttackMeeleType meeleType;



    public GameObject attackBoxes;
}

public enum AttackMeeleType
{
    NONE,
    STOP_MOTION
}
