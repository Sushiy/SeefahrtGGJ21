using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestTextProcessor : ScriptableObject
{
    public abstract string Process(string InputText, QuestCompletionParameters Params);
}
