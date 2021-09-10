using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastMove : MonoBehaviour
{
    private Ray ray = new Ray();
    private RaycastHit hitPoint = new RaycastHit();

    private bool isTeleportable = false;
    private bool isMoveable = false;

    [SerializeField]
    private WUC_TouchMove touchMove;

    private MouseDragRotate dragRotate = null;

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

            if (Physics.Raycast(ray.origin, ray.direction, out hitPoint, Mathf.Infinity))
            {
                if (isMoveable && isTeleportable)
                {
                    touchMove.gameObject.SetActive(true);
                    touchMove.transform.position = hitPoint.point + Vector3.up * 0.001f;

                    if (Input.GetMouseButtonUp(0))
                    {
                        MoveManager.inst.SetPos(hitPoint.point);
                    }
                }
            }
            else
            {
                touchMove.gameObject.SetActive(false);
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
