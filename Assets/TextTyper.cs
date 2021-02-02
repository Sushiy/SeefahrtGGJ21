using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public float startDelay = 1.0f;
    public float typingInterval = 0.01f;

    private TMP_Text textObject;

    private void Awake()
    {
        textObject = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        textObject.text = text;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        int totalVisibleCharacters = textObject.GetTextInfo(textObject.text).characterCount;
        textObject.maxVisibleCharacters = 0;
        print("Total: " + totalVisibleCharacters + "Text:" + textObject.text);
        yield return new WaitForSecondsRealtime(startDelay);

        for(int i = 0; i < totalVisibleCharacters; i++)
        {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(typingInterval);
        }
    }
}
