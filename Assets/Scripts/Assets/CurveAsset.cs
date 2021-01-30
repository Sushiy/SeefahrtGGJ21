using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Curve", menuName = "Curve Asset")]
public class CurveAsset : ScriptableObject
{
    [SerializeField] private AnimationCurve _data;

    public AnimationCurve Curve => _data;
}