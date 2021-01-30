using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : MonoBehaviour
{
    public Text TitleTextElement;
    public Text ContentTextElement;

    public Image TellerIcon;

    public Button ConfirmButton;
    public Text ConfirmButtonText;

    private string[] ContentTexts;

    private QuestTextProcessor[] Processors;
    private QuestCompletionParameters CompletionParams;


    public virtual void Setup(QuestObjectiveAsset Asset, QuestCompletionParameters Params)
    {
        TitleTextElement.text = Asset.ObjectiveTitle;

        Processors = Asset.TextProcessors.ToArray();

        CompletionParams = Params;

        ContentTexts = Asset.ObjectiveTexts;
        StartCoroutine(DisplayFurtherText());

        TellerIcon.sprite = Asset.ObjectiveTeller;
        ConfirmButtonText.text = Asset.ConfirmText;

        ConfirmButton.onClick.AddListener(() => { Destroy(this.gameObject); });
    }

    private IEnumerator DisplayFurtherText()
    {
        int i = 0;
        ContentTextElement.text = "";
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

            ContentTextElement.text += NewContent;
            yield return new WaitForSeconds(0.5F);
            ContentTextElement.text += '\n';
        }
    }
}