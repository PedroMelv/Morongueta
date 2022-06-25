using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleLink : MonoBehaviour
{
    public PlayerInput pInput     {get; private set;}
    public PlayerMovement pMove   {get; private set;}
    public PlayerAttack pAttack   {get; private set;}
    public PlayerEffects pEffects {get; private set;}

    public static PlayerModuleLink i;

    void Awake()
    {
        i = this;

        pInput = GetComponent<PlayerInput>();
        pMove = GetComponent<PlayerMovement>();
        pAttack = GetComponent<PlayerAttack>();
        pEffects = GetComponent<PlayerEffects>();
    }
}
