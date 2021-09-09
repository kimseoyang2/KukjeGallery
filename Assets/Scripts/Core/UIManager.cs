using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBehavior<UIManager>
{
    [SerializeField]
    private UP_BasePage introPage;
    [SerializeField]
    private UP_BasePage inGamePage;

    private void Start ()
    {
        Init();
    }

    private void Init ()
    {
        introPage.gameObject.SetActive(true);
        inGamePage.gameObject.SetActive(false);

        MoveManager.inst.SetMovable(false);
        EventController.inst.OnIntroLastBtnClicked += InGamePageOn;
    }

    private void InGamePageOn()
    {
        
        introPage.gameObject.SetActive(false);
        inGamePage.gameObject.SetActive(true);

        Invoke("MovemanagerMoveable", 1);
        
    }

    private void MovemanagerMoveable()
    {
        MoveManager.inst.SetMovable(true);
    }
}
