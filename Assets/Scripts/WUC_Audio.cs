using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WUC_Audio : MonoBehaviour
{

    [SerializeField]
    private Button btn;


    [SerializeField]
    public UnityEvent onClickEvent;
    // Start is called before the first frame update
    private void Awake()
    {
        btn.onClick.AddListener(OnClickBtn);
    }

    private void OnClickBtn()
    {
        if (onClickEvent != null)
        {
            onClickEvent.Invoke();
        }
    }
}
