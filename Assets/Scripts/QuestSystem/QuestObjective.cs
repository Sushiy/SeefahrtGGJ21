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

    private void DrawLineToObjective(QuestObjective otherObjective, Color color, float Thickness)
    {
        if (otherObjective)
        {
            Vector3 T0, T1, M;

            M = (otherObjective.transform.position - transform.position) * 0.5F;

            float MagScaling = 0.5F;

            T0 = M * 0.75F + Vector3.up * (M.magnitude * MagScaling);
            T0 = T0 + transform.position;

            T1 = (transform.position - otherObjective.transform.position) * 0.5F;
            T1 = (T1 * 0.75F) + Vector3.up * (T1.magnitude * MagScaling);
            T1 = otherObjective.transform.position + T1;
#if UNITY_EDITOR
            Handles.DrawBezier(transform.position, otherObjective.transform.position, T0, T1,
                color, null, Thickness);
#endif

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.26f, 0.27f);
        Transform t = transform;
        Vector3 p = t.position;

        DrawLineToObjective(NextObjective, Gizmos.color, 10.0F);

        var allQuestObjectives = FindObjectsOfType<QuestObjective>();
        foreach (var questObjective in allQuestObjectives)
        {
            Gizmos.color = new Color(1f, 0.98f, 0.33f);
            if (questObjective != this && questObjective != NextObjective)
            {
                if (questObjective && questObjective.NextObjective == this)
                {
                    DrawLineToObjective(questObjective, new Color(1f, 0.98f, 0.33f), 10.0F);
                }
            }
        }
    }
}