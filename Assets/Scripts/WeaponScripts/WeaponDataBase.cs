using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : MonoBehaviour
{
    [SerializeField]private RangedWeapon[] rangedWeapons;

    public static WeaponDataBase i;
    

    void Awake()
    {
        if(i != null)
        {
            Destroy(this.gameObject);
            return;
        }

        i = this;
        DontDestroyOnLoad(this);

        for (int i = 0; i < rangedWeapons.Length; i++)
        {
            rangedWeapons[i].ID = i+1;
        }
    }

    public Weapon GetWeaponByID(int ID)
    {
        Weapon w = new RangedWeapon();

        for (int i = 0; i < rangedWeapons.Length; i++)
        {
            if(rangedWeapons[i].ID == ID)
            {
                w = rangedWeapons[i];
                break;
            }
        }

        return w;
    }

}
