using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureViewPoint : MonoBehaviour
{
    public WUC_TouchMove movePoint;
    public BGSoundManager bGSoundManager;




    [SerializeField]
    private Animator Dissolve;

    public void OnClick()
    {
        if (movePoint != null)
        {

            Vector3 targetPos = movePoint.transform.position;
            Vector3 targetEular = movePoint.transform.localEulerAngles;

            GameManager.inst.MoveWUC_Touch(targetPos, targetEular);
            MoveManager.inst.LookPic(this);



        }

        if (Dissolve != null)
        {
            Dissolve.Play("UnDissolve");
            bGSoundManager.BgmSoundOnOff(true);
            bGSoundManager.ChangeClip(0);
         
        }
    }
}
