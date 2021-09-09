using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_IntroPage : UP_BasePage
{
    [SerializeField]
    private UC_IntroComponent[] introCompos;

    public override void Init ()
    {
        for (int i = 0; i < introCompos.Length; i++)
        {
            int index = i;
            introCompos[i].OnBtnClickAction += () => OnClickIntroCompoBtn(index);
        }
    }

    private void OnClickIntroCompoBtn (int index)
    {
        if (introCompos.Length > index + 1)
        {
            introCompos[index + 1].gameObject.SetActive(true);
        }

        if(introCompos.Length == index + 1)
        {
            if(EventController.inst.OnIntroLastBtnClicked != null)
            {
                EventController.inst.OnIntroLastBtnClicked.Invoke();
            }
        }
    }

}
