using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastMove : MonoBehaviour
{
    private Ray ray = new Ray();
    private RaycastHit hitPoint = new RaycastHit();

    private bool isTeleportable = false;
    private bool isMoveable = true;

    [SerializeField]
    private WUC_TouchMove touchMove;
    [SerializeField]
    private string requireTag;

    private MouseDragRotate dragRotate = null;

    [SerializeField]
    private GameObject clickingPic = null;

    private void Start ()
    {
        dragRotate = GetComponent<MouseDragRotate>();

        dragRotate.OnDraged += (Vector2 test) =>
        {
            touchMove.gameObject.SetActive(false);
            isTeleportable = false;
        };
    }

    private void Update ()
    {
        if (isMoveable)
        {
            ray = MoveManager.inst.moveableCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hitPoint, Mathf.Infinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (isMoveable && isTeleportable && !MobileCheck.isMobile())
                {
                    if (hitPoint.transform.CompareTag(requireTag))
                    {
                        touchMove.gameObject.SetActive(true);
                        touchMove.transform.position = hitPoint.point + Vector3.up * 0.001f;

                        if (Input.GetMouseButtonUp(0))
                        {
                            MoveManager.inst.SetPos(hitPoint.point);
                        }

                        clickingPic = null;
                    }
                    else
                    {
                        touchMove.gameObject.SetActive(false);
                    } 
                }

                if(hitPoint.transform.GetComponent<PictureViewPoint>() != null)
                {
                    PictureViewPoint picture = hitPoint.transform.GetComponent<PictureViewPoint>();

                    if(clickingPic != picture.gameObject)
                    {
                        clickingPic = null;
                    }

                    if(Input.GetMouseButtonDown(0) && clickingPic == null)
                    {
                        clickingPic = picture.gameObject;
                    }

                    if(Input.GetMouseButtonUp(0) && clickingPic != null)
                    {
                        picture.OnClick();
                    }
                }
                else
                {
                    clickingPic = null;
                } 
            }
            else
            {
                touchMove.gameObject.SetActive(false);
                clickingPic = null;
            }
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
        {
            isTeleportable = true;
        }
    }

    public void SetMoveable (bool moveable)
    {
        isMoveable = moveable;
    }
}
