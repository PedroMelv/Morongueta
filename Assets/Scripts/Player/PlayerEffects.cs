using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]private LineRenderer targetLine;

    [SerializeField]private TrailRenderer playerTrail;
    private float trailSavedTime;

    private PlayerModuleLink PML;

    void Start()
    {
        PML = PlayerModuleLink.i;
        trailSavedTime = playerTrail.time;
    }

    public void DrawTargetLine(Vector3 startPos, Vector3 endPos)
    {
        endPos = new Vector3(endPos.x, startPos.y, endPos.z);

        targetLine.SetPosition(0, startPos);
        targetLine.SetPosition(1, endPos);
    }

    #region Player Trail Things

    public void ChangePlayerTrailActive(bool b)
    {
        if(b)
            playerTrail.emitting = true;
        else
            StartCoroutine(EPlayerTrailDisable());
    }

    private IEnumerator EPlayerTrailDisable()
    {
        playerTrail.time = .0f;
        yield return new WaitForEndOfFrame();
        playerTrail.emitting = false;
        playerTrail.time = trailSavedTime;
    }

    #endregion
}
