using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]private LineRenderer targetLine;

    private PlayerModuleLink PML;

    void Start()
    {
        PML = PlayerModuleLink.i;
    }

    public void DrawTargetLine(Vector3 startPos, Vector3 endPos)
    {
        endPos = new Vector3(endPos.x, startPos.y, endPos.z);

        targetLine.SetPosition(0, startPos);
        targetLine.SetPosition(1, endPos);
    }
}
