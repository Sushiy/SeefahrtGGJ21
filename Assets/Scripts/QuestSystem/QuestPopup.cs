using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPopup : MonoBehaviour
{
    public TMP_Text TitleTextElement;
    public TMP_Text contentTextElement;
    public TextTyper contentTyper;

    public Image TellerIcon;

    public Button ConfirmButton;
    public TMP_Text ConfirmButtonText;

    private string[] ContentTexts;

    private QuestTextProcessor[] Processors;
    private QuestCompletionParameters CompletionParams;


    public virtual void Setup(QuestObjectiveAsset Asset, QuestCompletionParameters Params)
    {
        TitleTextElement.text = Asset.ObjectiveTitle;

        Processors = Asset.TextProcessors.ToArray();

        CompletionParams = Params;

        ContentTexts = Asset.ObjectiveTexts;
        //StartCoroutine(DisplayFurtherText());
        contentTyper.SetText(ProcessString(ContentTexts[0]));

        TellerIcon.sprite = Asset.ObjectiveTeller;
        ConfirmButtonText.text = Asset.ConfirmText;

        ConfirmButton.onClick.AddListener(() =>
        {
            PopupOpenButton.PopPopup(this);
            
            Destroy(this.gameObject);
        });

        PopupOpenButton.PushPopup(this);
    }

    private IEnumerator DisplayFurtherText()
    {
        int i = 0;
        contentTextElement.text = "";
        for (; i < ContentTexts.Length; ++i)
        {
            string NewContent = ContentTexts[i];
            foreach (var questTextProcessor in Processors)
            {
                string previousProcess;
                do
                {
                    previousProcess = NewContent;
                    NewContent = questTextProcessor.Process(NewContent, CompletionParams);
                } while (NewContent != previousProcess);
            }

            contentTextElement.text += NewContent;
            yield return new WaitForSeconds(0.5F);
            contentTextElement.text += '\n';
        }
    }

    private string ProcessString(string input)
    {
        string output = input;
        foreach (var questTextProcessor in Processors)
        {
            string previousProcess;
            do
            {
                previousProcess = output;
                output = questTextProcessor.Process(output, CompletionParams);
            } while (output != previousProcess);
        }

        return output;
    }
}