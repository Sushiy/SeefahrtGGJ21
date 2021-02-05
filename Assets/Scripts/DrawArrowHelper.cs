using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrowHelper : MonoBehaviour
{
    public static void DrawLine(Vector3 start, Vector3 end, Color color, bool reverseTip = false)
    {
        Vector3 dir = (end - start).normalized;
        Debug.DrawLine(start, end, color);
        if(reverseTip)
        {
            Debug.DrawRay(start, Quaternion.Euler(0, 30, 0) * dir * 0.33f, color);
            Debug.DrawRay(start, Quaternion.Euler(0, -30, 0) * dir * 0.33f, color);
        }
        else
        {
            Debug.DrawRay(end, Quaternion.Euler(0, 30, 0) * -dir * 0.33f, color);
            Debug.DrawRay(end, Quaternion.Euler(0, -30, 0) * -dir * 0.33f, color);
        }
    }

    public static void DrawRay(Vector3 start, Vector3 dir, Color color, bool reverseTip = false)
    {
        DrawLine(start, start + dir, color, reverseTip);
    }
}
