using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{
    public float damage { get; set; }
    public LayerMask hitLayer;

    private bool hitted;

    void Update()
    {
        if(!hitted)
            CheckHit();
    }

    public void Reset(float damage)
    {
        this.damage = damage;
        hitted = false;
    }

    private void CheckHit()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f, hitLayer);
        if (hit.collider != null)
        {
            IDamagable d = hit.collider.gameObject.GetComponent<IDamagable>();
            Debug.Log("hitted");
            if (d != null)
            {
                d.TakeDamage(damage, out bool killed);
            }
            hitted = true;

        }
    }
}
