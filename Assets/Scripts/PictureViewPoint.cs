using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureViewPoint : MonoBehaviour
{
    public WUC_TouchMove movePoint;
    public WUC_TouchMove teleportPoint;
    public BGSoundManager bGSoundManager;
    public bool isMove;



    [SerializeField]
    private Animator Dissolve;


    public void Update()
    {
        if (isMove)
        {
            TeleportToPlayer();
        }
    }
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

    public void TeleportToPlayer()
    {
        if (teleportPoint != null)
        {

            Vector3 targetPos = teleportPoint.transform.position;
            Vector3 targetEular = teleportPoint.transform.localEulerAngles;

            GameManager.inst.MoveWUC_Touch(targetPos, targetEular);
            MoveManager.inst.LookPic(this);



        }
    }
}
