using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour
{

    Material mat;
    [SerializeField]
    private bool isDissoved = false;
    [SerializeField]
    float endTime = 2f;
    [SerializeField]
    float progressTime;


    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {

        if (isDissoved)
        {
            progressTime += Time.deltaTime;
            if (progressTime < 2.0f)
            {
                endTime = progressTime;
            }
            else
            {

                isDissoved = false;
            }

            mat.SetFloat("_DissolveAmount", Mathf.Lerp(0f, 1f, endTime/3));
        }



        else
        {

            progressTime = 0f;
            endTime = 0f;
        }

    }
}
