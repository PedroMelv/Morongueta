using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : MonoBehaviour
{
    [SerializeField] private RangedWeapon[] rangedWeapons;
    [SerializeField] private MeeleWeapon[] meeleWeapons;

    private List<Weapon> totalWeapons = new List<Weapon>();

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
            totalWeapons.Add(rangedWeapons[i]);
        }

        for (int i = 0; i < meeleWeapons.Length; i++)
        {
            totalWeapons.Add(meeleWeapons[i]);
        }

        for (int i = 0; i < totalWeapons.Count; i++)
        {
            totalWeapons[i].ID = i + 1;
        }


    }

    public Weapon GetWeaponByID(int ID)
    {
        for (int i = 0; i < totalWeapons.Count; i++)
        {
            if(totalWeapons[i].ID == ID)
            {
                return totalWeapons[i];
            }
        }

        return null;
    }

}
