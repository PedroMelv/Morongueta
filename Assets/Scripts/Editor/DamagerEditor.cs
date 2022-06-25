using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Damager))]
public class DamagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Damager d = (Damager)target;

        

        if (d.HasProfile())
        {
            EditorGUI.BeginChangeCheck();
            Vector3 size = EditorGUILayout.Vector3Field("DamageBoxSizes", d.GetProfile().damageBoxSizes);
            Vector3 offset = EditorGUILayout.Vector3Field("DamageBoxOffset", d.GetProfile().damageBoxOffset);
            

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Size and offset Change");
                d.EditProfile(size, offset);

                EditorUtility.SetDirty(target);
            }
        }

        if(GUILayout.Button("Create New Profile"))
        {
            if (!Directory.Exists(Application.dataPath + "/DamageProfiles"))
                Directory.CreateDirectory(Application.dataPath + "/DamageProfiles");

            int numb = Directory.GetFiles(Application.dataPath + "/DamageProfiles", "*.asset", SearchOption.TopDirectoryOnly).Length + 1;
            Debug.Log(Application.dataPath);
            d.CreateProfile("Assets", numb);
        }
    }
}
