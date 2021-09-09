using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_IntroComponent : UC_BaseComponent
{
    [SerializeField]
    private RawImage img = null;
    [SerializeField]
    private Button btn = null;

    public Action OnBtnClickAction = null;

    public override void BindDelegates ()
    {
        btn.onClick.AddListener(OnBtnClicked);
    }

    private void OnBtnClicked()
    {
        if(OnBtnClickAction != null)
        {
            OnBtnClickAction.Invoke();
        }
    }
}
