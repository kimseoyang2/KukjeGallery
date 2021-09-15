
using UnityEngine;
using System.Runtime.InteropServices;

public class WebRequest : MonoBehaviour
{

    [SerializeField]
    private int btnIndex;
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str);

    [DllImport("__Internal")]
    private static extern void paint_Popup(int btnIndex);


    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture);

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
 
    public void CallJSEvent()
    {

        paint_Popup(btnIndex);
        Debug.Log(string.Format("paint_Popup({0}) is called", codeIndex));

    }

}
