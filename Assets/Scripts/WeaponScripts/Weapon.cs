using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon 
{
    [Header("General Things")]
    public string name;
    [HideInInspector]public int ID;

    public float damage;

}
