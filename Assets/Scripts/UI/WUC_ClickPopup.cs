using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WUC_ClickPopup : MonoBehaviour
{
    [SerializeField]
    private Button btn;

    [SerializeField]    
    private int codeIndex;
    /// <summary>
    /// Index of art asset
    /// </summary>
    [SerializeField]
    private int btnIndex;
    /// <summary>
    /// Index of button of art asset
    /// </summary>

    private void Awake ()
    {
        btn.onClick.AddListener(() => OnClickBtn(codeIndex));
    }

    private void OnClickBtn(int codeIndex)
    {


        Application.ExternalEval(string.Format("paint_popup({0})", codeIndex));
        Debug.Log(string.Format("JS Popup({0}) is called", codeIndex));

    }
    
}
