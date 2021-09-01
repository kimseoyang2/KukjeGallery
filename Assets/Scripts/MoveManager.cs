using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardMove), typeof(DragRotate))]
public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private KeyboardMove keyboardMove = null;
    [SerializeField]
    private DragRotate dragRotate = null;
    [SerializeField]
    private Joystick joystickL = null;
    [SerializeField]
    private Joystick joystickR = null;

    [SerializeField]
    private GameObject moveableObj = null;
    [SerializeField]
    private GameObject moveableCamera = null;

    [SerializeField, Range(0, 1)]
    private float moveSpeed;
    [SerializeField, Range(0, 1)]
    private float rotateSpeed;

    private void Awake ()
    {
        if (!MobileCheck.isMobile())
        {
            keyboardMove.OnMoveKeyClicked += Move;
            dragRotate.OnDraged += Rotate;
            keyboardMove.SetMoveable(true);

            joystickL.SetJoysticVisible(false);
            joystickR.SetJoysticVisible(false);
        }
        else
        {
            joystickL.OnJoysticMove += Move;
            joystickL.OnJoysticRotate += Rotate;

            joystickR.OnJoysticMove += Move;
            joystickR.OnJoysticRotate += Rotate;

            joystickL.SetJoysticVisible(true);
            joystickR.SetJoysticVisible(true);

            keyboardMove.SetMoveable(false);
        }
    }

    private void Move (Vector2 moveDelta)
    {
        if (moveableObj != null)
        {
            moveableObj.transform.position += moveableObj.transform.forward * moveDelta.y * moveSpeed * 0.1f;
            moveableObj.transform.position += moveableObj.transform.right * moveDelta.x * moveSpeed * 0.1f;
        }
    }

    private void Rotate (Vector2 rotateDelta)
    {
        if (moveableObj != null)
        {
            moveableCamera.transform.eulerAngles += -Vector3.right * rotateDelta.y * rotateSpeed * 0.1f;
            moveableObj.transform.eulerAngles += Vector3.up * rotateDelta.x * rotateSpeed * 0.1f;
        }
    }
}
