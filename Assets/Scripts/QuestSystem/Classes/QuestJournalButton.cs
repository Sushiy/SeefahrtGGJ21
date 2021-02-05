using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuestJournalButton : MonoBehaviour
{
    private Button _selfButton;
    private QuestSubsystem _questSubsystem;

    private Sprite closedImage;
    public Sprite openImage;

    // Start is called before the first frame update
    void Start()
    {
        _selfButton = GetComponent<Button>();
        closedImage = _selfButton.image.sprite;

        PopupOpenButton.GlobalPopupHandler.AddListener((anyPopupOpen) => MakePopupInteractable(!anyPopupOpen));

        _questSubsystem = FindObjectOfType<QuestSubsystem>();
        if (_questSubsystem)
        {
            _questSubsystem.onJournalOpened.AddListener(opened =>
            {
                MakePopupInteractable(!opened);
                if (openImage && closedImage)
                {
                    _selfButton.image.sprite = opened ? openImage : closedImage;
                }
            });
        }
    }

    void MakePopupInteractable(bool isClosed)
    {
        _selfButton.enabled = isClosed;
        _selfButton.interactable = isClosed;
        
    }
}