using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Compass", menuName = "Quest Text Processors/Player Objective Direction")]
public class QuestPlayerObjectiveDirection : QuestTextCompassProcessor
{
    /// <inheritdoc />
    public override float CalculateAngle(QuestCompletionParameters Params)
    {
        return Vector3.Angle(Params.CompletionPlayerLocation - Params.CompletedObjective.transform.position ,
            Vector3.forward) % 360.0F;
    }
}
