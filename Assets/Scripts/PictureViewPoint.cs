using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureViewPoint : MonoBehaviour
{
    public WUC_TouchMove movePoint;
    public GameObject maincamera;

    [SerializeField]
    private bool isOrigin;

    public void OnClick ()
    {
        if (movePoint != null)
        {
            if (isOrigin==true)
            {
                Vector3 targetPos = movePoint.transform.position;
                Vector3 targetEular = movePoint.transform.localEulerAngles;

                GameManager.inst.MoveWUC_Touch(targetPos, targetEular);
                MoveManager.inst.LookPic(this);
            }


            else if (isOrigin == false)
            {
                Vector3 targetPos = movePoint.transform.position;
                Vector3 targetEular = movePoint.transform.localEulerAngles;

                GameManager.inst.MoveWUC_Touch(targetPos, targetEular);
                MoveManager.inst.LookPic(this);
                maincamera.transform.position =new Vector3(transform.position.x, 3.0f, transform.position.z);
            }
        }
    }
}
