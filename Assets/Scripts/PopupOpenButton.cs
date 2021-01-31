using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupOpenButton : MonoBehaviour
{
    /// <summary>
    /// True returns if any popup is still open, false - no popup is open
    /// </summary>
    public static UnityEvent<bool> GlobalPopupHandler = new UnityEvent<bool>();

    private static List<MonoBehaviour> PopupStack = new List<MonoBehaviour>();

    public static void PushPopup(MonoBehaviour p)
    {
        if (!PopupStack.Contains(p))
        {
            PopupStack.Add(p);
            GlobalPopupHandler.Invoke(IsAnyPopupOnStack());
        }
    }

    public static void PopPopup(MonoBehaviour p)
    {
        PopupStack.Remove(p);
        GlobalPopupHandler.Invoke(IsAnyPopupOnStack());
    }

    public static bool IsAnyPopupOnStack() => PopupStack.Count > 0;


    public GameObject popupPrefab;

    private Sprite _defaultGraphic;
    public Sprite popupOpenGraphic;

    private Button _selfButton;

    private void Start()
    {
        _selfButton = GetComponent<Button>();
        _defaultGraphic = _selfButton.image.sprite;

        GlobalPopupHandler.AddListener(isAnyPopupOpen => OnInteractableChange(!isAnyPopupOpen));

        if (popupPrefab && _selfButton)
        {
            _selfButton.onClick.AddListener(() =>
            {
                var go = GameObject.Instantiate(popupPrefab);
                var popup = go.GetComponent<PopupUI>();
                if (popup)
                {
                }
                else
                {
                    Destroy(go);
                }
            });
        }
    }

    protected virtual void OnInteractableChange(bool isInteractable)
    {
        _selfButton.enabled = isInteractable;
        _selfButton.interactable = isInteractable;
        if (popupOpenGraphic)
        {
            _selfButton.image.sprite = isInteractable ? _defaultGraphic : popupOpenGraphic;
        }
    }
}