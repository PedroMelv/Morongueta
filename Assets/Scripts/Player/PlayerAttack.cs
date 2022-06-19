using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Basics")]


    private PlayerModuleLink PML;

    void Start()
    {
        PML = PlayerModuleLink.i;
    }

    void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = PML.pInput.GetMousePos(transform.position);

        PML.pEffects.DrawTargetLine(startPos, endPos);
    }
}
