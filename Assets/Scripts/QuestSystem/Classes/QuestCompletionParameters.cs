using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletionParameters
{
    public GameObject PlayerGameObject { get; set; }
    public QuestObjective CompletedObjective { get; set; }
    
    public Vector3 CompletionLocation { get; set; }
    
    public Quaternion CompletionRotation { get; set; }
    
    public QuestObjective NextObjective { get; set; }
    
}
