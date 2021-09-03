using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchDragRotate : MonoBehaviour
{
    /// <summary>
    /// bool : drag start on gui
    /// Vector2 : touch delta pos
    /// </summary>
    public Action<bool, Vector2> OnTouchDrag = null;

    private Dictionary<int, bool> isTouchStartedOnGui = new Dictionary<int, bool>();
    private Coroutine drageUpdateCoroutine = null;

    private bool isRotateAble = false;

    //private void Awake ()
    //{
    //    MainSceneEvenetManager.OnMoveableChanged += SetTouchDragable;
    //}

    public void SetTouchDragable (bool isDragable)
    {
        isRotateAble = isDragable;
    }

    private void Update ()
    {
        if (isRotateAble)
        {
            TouchDragCheck();
        } 
    }

    private void TouchDragCheck()
    {
        if (Input.touchCount != 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Touch specificTouch = touch;

                if (!isTouchStartedOnGui.ContainsKey(touch.fingerId))
                {
                    isTouchStartedOnGui.Add(touch.fingerId, EventSystem.current.IsPointerOverGameObject(touch.fingerId));
                }

                if (touch.phase == TouchPhase.Began)
                {
                    specificTouch.deltaPosition = Vector2.zero;
                    isTouchStartedOnGui[touch.fingerId] = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    isTouchStartedOnGui.Remove(touch.fingerId);
                }

                if (OnTouchDrag != null)
                {
                    OnTouchDrag.Invoke(isTouchStartedOnGui[touch.fingerId], specificTouch.deltaPosition);
                }
            }
        }
    }
}
