using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class QuestObjective : MonoBehaviour
{
    [SerializeField] private QuestObjectiveAsset Asset;

    public QuestObjective NextObjective;

    public virtual QuestObjectiveAsset GetAsset()
    {
        return Asset;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.26f, 0.27f);
        Transform t = transform;
        Vector3 p = t.position;

        if (NextObjective)
        {
            Vector3 T0, T1, M;

            M = (NextObjective.transform.position - transform.position) * 0.5F;

            float MagScaling = 0.5F;
            
            T0 = M * 0.75F + Vector3.up * (M.magnitude * MagScaling);
            T0 = T0 + transform.position;
            
            T1 = (transform.position - NextObjective.transform.position) * 0.5F;
            T1 = (T1 * 0.75F) + Vector3.up * (T1.magnitude * MagScaling);
            T1 = NextObjective.transform.position + T1;
            
            Handles.DrawBezier(transform.position, NextObjective.transform.position, T0, T1,
                Gizmos.color, null, 10.0F);
        }
    }
}