using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public float startDelay = 0.0f;
    public float typingInterval = 0.05f;

    private TMP_Text textObject;

    private void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void SetText(string text, bool shouldType = true)
    {
        StopAllCoroutines();
        textObject.text = text;
        if(shouldType)
            StartCoroutine(TypeText());
        else
            textObject.maxVisibleCharacters = textObject.GetTextInfo(textObject.text).characterCount;
    }

    IEnumerator TypeText()
    {
        int totalVisibleCharacters = textObject.GetTextInfo(textObject.text).characterCount;
        textObject.maxVisibleCharacters = 0;
        print("Total: " + totalVisibleCharacters + "Text:" + textObject.text);
        yield return new WaitForSecondsRealtime(startDelay);

        for(int i = 0; i < totalVisibleCharacters + 1; i++)
        {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(typingInterval);
        }
    }
}
