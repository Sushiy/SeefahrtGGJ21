using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Replacement", menuName = "Quest Text Processors/Single")]
public class QuestTextSimpleProcessor : QuestTextProcessor
{
    public string Text;
    
    /// <inheritdoc />
    public override string Process(string InputText, QuestCompletionParameters Params)
    {
        return InputText.Replace(Tag, Bold(Colorize(Text)));
    }
}
