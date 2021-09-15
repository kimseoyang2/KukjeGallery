using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureViewPoint : MonoBehaviour
{
    public WUC_TouchMove movePoint;

    public void OnClick ()
    {
        if (movePoint != null)
        {
            Vector3 targetPos = movePoint.transform.position;
            Vector3 targetEular = movePoint.transform.localEulerAngles;

            GameManager.inst.MoveWUC_Touch(targetPos, targetEular);
            MoveManager.inst.LookPic(this);
        }
    }
}
