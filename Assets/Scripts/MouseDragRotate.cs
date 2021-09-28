using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragRotate : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector2 mouseDeltaPos;
    private bool isClicked = false;
    private bool isDragging = false;
    private bool isOnGui = false;

    private Coroutine drageUpdateCoroutine = null;

    public Action<Vector2> OnDraged = null;

    private bool dragable = false;

    //private void Awake ()
    //{
    //    MainSceneEvenetManager.OnMoveableChanged += SetMouseDragable;
    //}

    public void SetMouseDragable (bool isDragable)
    {
        dragable = isDragable;
        //if (isDragable)
        //{

        //    if (drageUpdateCoroutine == null)
        //    {
        //        drageUpdateCoroutine = StartCoroutine(DrageUpdateRoutine());
        //    }
        //}
        //else
        //{
        //    if (drageUpdateCoroutine != null)
        //    {
        //        StopCoroutine(drageUpdateCoroutine);
        //        drageUpdateCoroutine = null;
        //    }
        //}
    }

    private void Update ()
    {
        if(dragable)
        {
            UpdateDrag();
        }
    }

    private void UpdateDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isOnGui = true;
        }
        else
        {
            isOnGui = false;
        }

        if (Input.GetMouseButtonDown(0) && !isOnGui)
        {
            isClicked = true;
        }

        if (Input.GetMouseButton(0) && isClicked)
        {
            mouseDeltaPos = mousePos - (Vector2)Input.mousePosition;

            if (mouseDeltaPos != Vector2.zero)
            {
                isDragging = true;
            }
            else
            {
                isDragging = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            isDragging = false;

            mouseDeltaPos = Vector2.zero;
        }

        if (isDragging)
        {
            if (OnDraged != null)
            {
                OnDraged.Invoke(mouseDeltaPos);
            }
        }

        mousePos = Input.mousePosition;
    }

    public void ResetDrag()
    {
        isClicked = false;
        isDragging = false;

        mouseDeltaPos = Vector2.zero;
    }

    //private IEnumerator DrageUpdateRoutine ()
    //{
    //    while (true)
    //    {
           

    //        yield return null;
    //    }
    //}
}
