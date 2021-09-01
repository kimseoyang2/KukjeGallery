using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRotate : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector2 mouseDeltaPos;
    private bool isClicked = false;
    private bool isDragging = false;
    private bool isOnGui = false;

    public Action<Vector2> OnDraged = null;

    private void Update ()
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

        if(Input.GetMouseButton(0) && isClicked)
        {
            mouseDeltaPos = mousePos - (Vector2)Input.mousePosition;

            if (mouseDeltaPos != Vector2.zero)
            {
                isDragging = true;
            }else
            {
                isDragging = false;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            isDragging = false;

            mouseDeltaPos = Vector2.zero;
        }

        if(isDragging)
        {
            if(OnDraged != null)
            {
                OnDraged.Invoke(mouseDeltaPos);
            }
        }

        mousePos = Input.mousePosition;
    }

}
