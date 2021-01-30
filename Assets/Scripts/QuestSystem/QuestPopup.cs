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

    public virtual void Setup(QuestObjectiveAsset Asset)
    {
        TitleTextElement.text = Asset.ObjectiveTitle;

        ContentTexts = Asset.ObjectiveTexts;
        StartCoroutine(DisplayFurtherText());

        TellerIcon.sprite = Asset.ObjectiveTeller;
        ConfirmButtonText.text = Asset.ConfirmText;
        
        ConfirmButton.onClick.AddListener(() =>
        {
            Destroy(this.gameObject);
        });
    }

    private IEnumerator DisplayFurtherText()
    {
        int i = 0;
        for (; i < ContentTexts.Length; ++i)
        {
            ContentTextElement.text += ContentTexts[i];
            yield return new WaitForSeconds(0.5F);
        }
    }
}