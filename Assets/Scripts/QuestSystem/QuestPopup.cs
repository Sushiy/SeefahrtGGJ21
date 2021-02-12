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

    public Button CloseButton;
    public Button previousButton;
    public Button nextButton;

    private List<JournalPage> pages;

    public TMP_Text pagecounter;
    public bool open;

    private int textIndex = 0;
    private int lastTypedPage = -1;

    struct JournalPage
    {
        public string title;
        public string text;
        public Sprite icon;
        public QuestCompletionParameters questParams;
        public QuestTextProcessor[] processors;

        public JournalPage(string title, string text, Sprite icon, QuestCompletionParameters questParams, QuestTextProcessor[] processors)
        {
            this.title = title;
            this.text = text;
            this.icon = icon;
            this.questParams = questParams;
            this.processors = processors;
        }
    }

    private void Awake()
    {
        open = true;
        pages = new List<JournalPage>();
    }

    public void OpenJournal()
    {
        gameObject.SetActive(true);
        ShowPage(textIndex);
        open = true;
    }

    public virtual void AddToJournalAndShow(QuestObjectiveAsset Asset, QuestCompletionParameters Params)
    {
        if (Asset.ObjectiveTexts.Length == 0) return;

        textIndex = pages.Count;

        for (int i = 0; i < Asset.ObjectiveTexts.Length; i++)
        {
            pages.Add(new JournalPage(Asset.ObjectiveTitle, Asset.ObjectiveTexts[i], Asset.ObjectiveTeller, Params, Asset.TextProcessors.ToArray()));
        }

        ShowPage(textIndex);


        PopupOpenButton.PushPopup(this);
    }

    private void ShowPage(int i)
    {
        textIndex = i;
        nextButton.gameObject.SetActive(true);
        previousButton.gameObject.SetActive(true);
        if (textIndex == 0)
        {
            previousButton.gameObject.SetActive(false);
        }
        if(textIndex == pages.Count-1)
        {
            nextButton.gameObject.SetActive(false);
        }

        if(textIndex > lastTypedPage)
        {
            contentTyper.SetText(ProcessString(pages[textIndex].text, pages[i].processors, pages[i].questParams));
            lastTypedPage = textIndex;
        }
        else
        {
            contentTyper.SetText(ProcessString(pages[textIndex].text, pages[i].processors, pages[i].questParams), false);
        }
        pagecounter.text = (textIndex + 1) + "/" + pages.Count;
        TitleTextElement.text = pages[i].title;
        TellerIcon.sprite = pages[i].icon;
    }

    private string ProcessString(string input, QuestTextProcessor[] processors, QuestCompletionParameters completionParams)
    {
        string output = input;
        foreach (var questTextProcessor in processors)
        {
            string previousProcess;
            do
            {
                previousProcess = output;
                output = questTextProcessor.Process(output, completionParams);
            } while (output != previousProcess);
        }

        return output;
    }

    public void Close()
    {
        PopupOpenButton.PopPopup(this);
        open = false;

        gameObject.SetActive(false);
    }

    public void NextPage()
    {
        if(textIndex < pages.Count-1)
        {
            ShowPage(textIndex + 1);
        }
    }

    public void PreviousPage()
    {
        if (textIndex > 0)
        {
            ShowPage(textIndex - 1);
        }
    }
}