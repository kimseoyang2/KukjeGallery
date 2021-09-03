using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Joystic joysticVal = Joystic.move;
    public Action<Vector2> OnJoysticMove = null;
    public Action<Vector2> OnJoysticRotate = null;

    [SerializeField]
    private RectTransform stickBG = null;
    [SerializeField]
    private RectTransform stick = null;

    [SerializeField, Range(10, 150f)]
    private float stickRange = 150f;

    private Vector2 inputVec;

    private const float moveMult = 2.5f;
    private const float rotateMult = 15f;

    public void SetJoysticVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    private void Update ()
    {
        if (OnJoysticMove != null && inputVec != Vector2.zero)
        {
            if (joysticVal == Joystic.move)
            {
                OnJoysticMove.Invoke(inputVec * moveMult);
            }
            else if (joysticVal == Joystic.rotate)
            {
                OnJoysticRotate.Invoke(inputVec * rotateMult);
            }
        }
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        ControlJoystick(eventData);
    }

    public void OnDrag (PointerEventData eventData)
    {
        ControlJoystick(eventData);
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
        inputVec = Vector2.zero;
    }

    public void ControlJoystick (PointerEventData eventData)
    {
        Vector2 inputDir = eventData.position - (Vector2)stickBG.position;

        Vector2 clampedDir = inputDir.magnitude < stickRange ? inputDir : inputDir.normalized * stickRange;

        stick.anchoredPosition = clampedDir;

        inputVec = clampedDir / stickRange;


    }




}
