using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestTextProcessor : ScriptableObject
{
    public string Tag = "{}";

    [Header("Formatting)")]
    public bool ColorizeReplacement;
    public Color ReplacementColor; 
    public bool MakeReplacementBold;

    public abstract string Process(string InputText, QuestCompletionParameters Params);

    public string Bold(string inText)
    {
        if (MakeReplacementBold)
        {
            return "<b>" + inText + "</b>";
        }

        return inText;
    }
    
    public string Colorize(string inText)
    {
        if (!ColorizeReplacement) return inText;
        return "<color=#"+ColorUtility.ToHtmlStringRGBA(ReplacementColor)+">" + inText + "</color>";
    }
}