﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Compass", menuName = "Quest Text Processors/Compass")]
public class QuestTextCompassProcessor : QuestTextProcessor
{
    [System.Serializable]
    struct FCompassDirection
    {
        [Range(0, 360)] public float MinAngle;
        [Range(0, 360)] public float MaxAngle;

        public string ReplacementText;
    }

    [SerializeField] private List<FCompassDirection> DirectionMapping = new List<FCompassDirection>(
        new[]
        {
            new FCompassDirection() {MinAngle = 315, MaxAngle = 360, ReplacementText = "North"},
            new FCompassDirection() {MinAngle = 0, MaxAngle = 45.0F, ReplacementText = "North"},
            new FCompassDirection() {MinAngle = 45, MaxAngle = 135, ReplacementText = "East"},
            new FCompassDirection() {MinAngle = 135, MaxAngle = 225, ReplacementText = "South"},
            new FCompassDirection() {MinAngle = 225, MaxAngle = 315, ReplacementText = "West"},
        }
    );

    bool GetCompassDirectionFroMAngle(float Angle, out FCompassDirection Direction)
    {
        foreach (FCompassDirection compassDirection in DirectionMapping)
        {
            if (Angle > compassDirection.MinAngle && Angle <= compassDirection.MaxAngle)
            {
                Direction = compassDirection;
                return true;
            }
        }

        Direction = new FCompassDirection();
        return false;
    }

    /// <inheritdoc />
    public override string Process(string InputText, QuestCompletionParameters Params)
    {
        if (Params.NextObjective)
        {
            float angle = Vector3.Angle(Params.NextObjective.transform.position - Params.CompletionPlayerLocation,
                Vector3.forward) % 360.0F;
            FCompassDirection foundCompassDirection;
            if (GetCompassDirectionFroMAngle(angle, out foundCompassDirection))
            {
                return InputText.Replace(base.Tag, foundCompassDirection.ReplacementText);
            }
        }

        return InputText;
    }
}