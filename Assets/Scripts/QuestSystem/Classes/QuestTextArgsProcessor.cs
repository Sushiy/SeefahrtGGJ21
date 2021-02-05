using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Replacement", menuName = "Quest Text Processors/Argument Parser")]
public class QuestTextArgsProcessor : QuestTextProcessor
{
    [System.Serializable]
    public struct FArgumentToString
    {
        public string Key;
        public string Value;
    }

    public int ArgumentIndex = 0;
    public bool KeyIsValue = false;
    public FArgumentToString[] ArgumentToString;

    bool HasKey(string Key, out string FoundArg)
    {
        if (KeyIsValue)
        {
            FoundArg = Key;
            return true;
        }
        else
        {
            foreach (var argumentToString in ArgumentToString)
            {
                if (Key == argumentToString.Key)
                {
                    FoundArg = argumentToString.Value;
                    return true;
                }
            }

            FoundArg = null;
            return false;
        }
    }

    /// <inheritdoc />
    public override string Process(string InputText, QuestCompletionParameters Params)
    {
        int TagFrom, TagTo;
        string Arg = GetTagArgument(InputText, 0, out TagFrom, out TagTo);
        string foundArg;

        if (Arg != null && HasKey(Arg, out foundArg))
        {
            if (TagFrom < 0) return InputText;
            InputText = InputText.Remove(TagFrom, TagTo - TagFrom + 1);
            return InputText.Insert(TagFrom, Bold(Colorize(foundArg)));
        }

        return InputText;
    }
}