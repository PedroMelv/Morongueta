using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{
    [SerializeField] private float attackSize;

    [SerializeField] private DamagerInfoProfile damagerProfile;
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
        Physics.BoxCast(transform.position + damagerProfile.damageBoxOffset, damagerProfile.damageBoxSizes / 2f, transform.forward, out RaycastHit hit, transform.rotation, attackSize, hitLayer);
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



    #region Utils


    public void OnDrawGizmos()
    {
        if (damagerProfile == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + damagerProfile.damageBoxOffset + transform.forward, damagerProfile.damageBoxSizes * attackSize);
    }

    public bool HasProfile()
    {
        return damagerProfile != null;
    }

    public void EditProfile(Vector3 size, Vector3 offset)
    {
        damagerProfile.damageBoxSizes = size;
        damagerProfile.damageBoxOffset = offset;
    }

    public DamagerInfoProfile GetProfile()
    {
        return damagerProfile;
    }

    public void CreateProfile(string path, int numb)
    {
        DamagerInfoProfile newProfile = ScriptableObject.CreateInstance<DamagerInfoProfile>();

        UnityEditor.AssetDatabase.CreateAsset(newProfile, path + "/profile" + numb + ".asset");

        damagerProfile = newProfile;

        UnityEditor.AssetDatabase.MoveAsset("Assets" + "/profile" + numb + ".asset", "Assets" + "/DamageProfiles/profile" + numb + ".asset");

        UnityEditor.AssetDatabase.Refresh();
    }

    #endregion
}
