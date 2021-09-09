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
        btn.onClick.AddListener(() => OnClickBtn(codeIndex, btnIndex));
    }

    private void OnClickBtn(int code, int btn)
    {
        Debug.Log(string.Format("JS Popup({0}, {1}) is called", code, btn));
    }
}
