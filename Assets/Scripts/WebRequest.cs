using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRequest : MonoBehaviour
{

    [SerializeField]
    private int codeIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void CallJSEvent(int codeIndex)
    {
        Application.ExternalEval(string.Format("paint_Popup({0})", codeIndex));
        Debug.Log(string.Format("paint_Popup({0}) is called", codeIndex));

    }

}
