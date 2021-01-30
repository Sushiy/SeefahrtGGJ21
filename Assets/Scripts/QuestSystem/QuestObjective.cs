using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class QuestObjective : MonoBehaviour
{
    [SerializeField]
    private QuestObjectiveAsset Asset;
    
    public virtual QuestObjectiveAsset GetAsset()
    {
        return Asset;
    }
    

}
