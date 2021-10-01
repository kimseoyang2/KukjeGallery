using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOn : MonoBehaviour
{

    public BGSoundManager BGSoundManager;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
         
            BGSoundManager.BgmSoundOnOff(true);

            


        }
    }
}
