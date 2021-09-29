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



    //달 키오스크 영상 재생 함수
    public void CallVidPlay(int codeIndex)
    {
        Application.ExternalEval(string.Format("***({0})", codeIndex));
        Debug.Log("키오스크 영상 재생");
    }
    //그림 상세보기 자바스크립트 호출함수
    public void CallJSEvent(int codeIndex)
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keabord inputs
        WebGLInput.captureAllKeyboardInput = false;
    #endif
        Application.ExternalEval(string.Format("paint_popup({0})", codeIndex));

        Debug.Log(string.Format("paint_Popup({0}) is called", codeIndex));

    }

}
