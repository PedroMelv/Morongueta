using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerModuleLink PML;

    [SerializeField]private KeyCode dashKey;

    void Start()
    {
        PML = PlayerModuleLink.i;
    }

    void Update()
    {
        PML.pMove.SetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        if(Input.GetKeyDown(dashKey)) PML.pMove.ExecuteDash();
        if(Input.GetKeyUp(dashKey)) PML.pMove.ReleaseDash();
    }

    public Vector3 GetMousePos(Vector3 pos)
    {
        Vector3 hitPoint = Vector3.zero;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, pos);

        if (p.Raycast(mouseRay, out float hitDist))
        {
            hitPoint = mouseRay.GetPoint(hitDist);
        }
        return hitPoint;
    }
}
