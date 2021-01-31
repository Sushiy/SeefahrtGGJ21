using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public Button ConfirmButton;
    public Button CancelButton;
    
    public UnityEvent<bool> Closed = new UnityEvent<bool>();

    private void Start()
    {
        PopupOpenButton.PushPopup(this);

        if (ConfirmButton)
        {
            ConfirmButton.onClick.AddListener(() => Close(true));
        }

        if (CancelButton)
        {
            CancelButton.onClick.AddListener((() => Close(false)));
        }
    }

    public void Close(bool bSuccess)
    {
        Closed.Invoke(bSuccess);
        PopupOpenButton.PopPopup(this);
        
        Destroy(gameObject);
    }
}
