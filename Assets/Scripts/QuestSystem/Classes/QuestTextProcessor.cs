using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestTextProcessor : ScriptableObject
{
    public string Tag = "{}";

    [Header("Formatting)")] public bool ColorizeReplacement;
    public Color ReplacementColor;
    public bool MakeReplacementBold;

    public string GetTagArgument(string InputText, int index, out int TagBeginIndex, out int TagEndIndex)
    {
        TagBeginIndex = -1;
        TagEndIndex = -1;
        if (InputText == null) return null;
        if (index < 0) return null;


        string TagWithoutEnd = Tag.Substring(0, Tag.Length - 1);
        TagBeginIndex = InputText.IndexOf(TagWithoutEnd);


        int EndOfTag = TagBeginIndex + TagWithoutEnd.Length;
        if (EndOfTag >= InputText.Length)
        {
            Debug.Log("Too long!" + EndOfTag + "/" + InputText.Length + ":" + InputText);
            return null;
        }
        string ArgString = InputText.Substring(EndOfTag);

        if (ArgString.Length > 0)
        {
            TagEndIndex = EndOfTag + ArgString.IndexOf('}');
            if (ArgString[0] == '(')
            {
                int indexClosing = ArgString.IndexOf(')');

                string[] Args = ArgString.Substring(1, indexClosing - 1).Split('|');
                if (index < Args.Length)
                {
                    return Args[index];
                }
            }
        }

        TagBeginIndex = -1;
        TagEndIndex = -1;
        return null;
    }

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
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(ReplacementColor) + ">" + inText + "</color>";
    }
}