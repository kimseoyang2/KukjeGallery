using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_InGameBtns : UC_BaseComponent
{
    [SerializeField]
    private Button bgmBtn;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private Button tipBtn;

    public Action OnClickExitAction;
    public Action OnClickTipAction;

    public override void BindDelegates ()
    {
        bgmBtn.onClick.AddListener(OnClickBGM);
        exitBtn.onClick.AddListener(OnClickExit);
        tipBtn.onClick.AddListener(OnClickTip);
    }

    private void OnClickBGM()
    {
        if(EventController.inst.OnBGMBtnClicked != null)
        {
            EventController.inst.OnBGMBtnClicked.Invoke();
        }
    }

    private void OnClickExit()
    {
        if(OnClickExitAction != null)
        {
            OnClickExitAction.Invoke();
        }
    }

    private void OnClickTip()
    {
        if(OnClickTipAction != null)
        {
            OnClickTipAction.Invoke();
        }
    }
}
