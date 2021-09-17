using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Texture newTex = GameManager.inst.GetTextureFromUri(TextureType.test01);
            cube.GetComponent<Renderer>().material.mainTexture = newTex;
        }
    }
}
