using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WUC_Interaction : MonoBehaviour
{
    [SerializeField]
    private Button btn;

    [SerializeField]
    public UnityEvent onClickEvent;

    private void Awake ()
    {
        btn.onClick.AddListener(OnClickBtn);
    }

    private void OnClickBtn()
    {
        if(onClickEvent != null)
        {
            onClickEvent.Invoke();
        }
    }
}
