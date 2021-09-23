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
    [SerializeField]
    private Sprite bgmBtnOnImg;
    [SerializeField]
    private Sprite bgmBtnOffImg;

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

        bgmBtn.image.sprite = BGSoundManager.inst.GetVolume() <= 0.5f ? bgmBtnOffImg : bgmBtnOnImg;
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
