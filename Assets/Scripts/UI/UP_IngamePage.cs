using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_IngamePage : UP_BasePage
{
    [SerializeField]
    private UC_IntroComponent introTip;
    [SerializeField]
    private UC_InGameBtns ingameBtns;

    public override void Init ()
    {
        TipDisable();

        introTip.OnBtnClickAction += TipDisable;
        ingameBtns.OnClickTipAction += TipEnable;

        ingameBtns.OnClickTipAction += TipEnable;
    }

    private void TipEnable()
    {
        introTip.gameObject.SetActive(true);
        ingameBtns.gameObject.SetActive(false);
    }

    private void TipDisable()
    {
        introTip.gameObject.SetActive(false);
        ingameBtns.gameObject.SetActive(true);
    }
}
