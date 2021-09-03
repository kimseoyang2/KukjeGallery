using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardMove), typeof(MouseDragRotate), typeof(TouchDragRotate))]
public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private KeyboardMove keyboardMove = null;
    [SerializeField]
    private MouseDragRotate mouseDragRotate = null;
    [SerializeField]
    private TouchDragRotate touchDragRotate = null;

    [SerializeField]
    private Joystick joystickL = null;
    [SerializeField]
    private Joystick joystickR = null;

    [SerializeField]
    private GameObject moveableObj = null;
    [SerializeField]
    private GameObject moveableCamera = null;

    [SerializeField, Range(0, 1)]
    private float moveSpeed = 0.5f;
    [SerializeField, Range(0, 1)]
    private float rotateSpeed = 0.5f;

    [SerializeField, Range(0.1f, 2f)]
    public float touchMoveDuration = 1f;

    private void Awake ()
    {
        MainSceneEvenetManager.OnPCMoveableChanged += keyboardMove.SetMoveable;
        MainSceneEvenetManager.OnPCMoveableChanged += mouseDragRotate.SetMouseDragable;

        MainSceneEvenetManager.OnMobileMoveableChanged += touchDragRotate.SetTouchDragable;
        MainSceneEvenetManager.OnMobileMoveableChanged += joystickL.SetJoysticVisible;
        MainSceneEvenetManager.OnMobileMoveableChanged += joystickR.SetJoysticVisible;

        if (!MobileCheck.isMobile())
        {
            keyboardMove.OnMoveKeyClicked += Move;
            mouseDragRotate.OnDraged += Rotate;

            joystickL.SetJoysticVisible(false);
            joystickR.SetJoysticVisible(false);

            MainSceneEvenetManager.OnPCMoveableChanged.Invoke(true);
            MainSceneEvenetManager.OnMobileMoveableChanged.Invoke(false);
        }
        else
        {
            touchDragRotate.OnTouchDrag += TouchRotate;

            joystickL.OnJoysticMove += Move;
            joystickL.OnJoysticRotate += Rotate;

            joystickR.OnJoysticMove += Move;
            joystickR.OnJoysticRotate += Rotate;

            joystickL.SetJoysticVisible(true);
            joystickR.SetJoysticVisible(true);

            MainSceneEvenetManager.OnPCMoveableChanged.Invoke(false);
            MainSceneEvenetManager.OnMobileMoveableChanged.Invoke(true);
        }

        

        MainSceneEvenetManager.SetPlayerNewPos += SetPos;
        MainSceneEvenetManager.SetPlayerNewEular += SetEular;
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

    private void TouchRotate (bool isStartedOnGui, Vector2 rotateDelta)
    {
        if (!isStartedOnGui)
        {
            moveableCamera.transform.eulerAngles += -Vector3.right * rotateDelta.y * rotateSpeed * 0.1f;
            moveableObj.transform.eulerAngles += Vector3.up * rotateDelta.x * rotateSpeed * 0.1f;
        }
    }

    public void SetPos(Vector3 newPos)
    {
        StartCoroutine(SetPosRoutine(newPos));
    }

    public void SetEular(Vector3 newEular)
    {
        StartCoroutine(SetEularRoutine(newEular));
    }

    private IEnumerator SetPosRoutine(Vector3 newPos)
    {
        float animTime = touchMoveDuration;
        float time = 0;

        Vector3 playerPosOrigin = moveableObj.transform.position;
        Vector3 targetPos;

        MainSceneEvenetManager.SetPlayerMoveable(false);
        while (time < animTime)
        {
            targetPos = Vector3.Lerp(playerPosOrigin, newPos, time / animTime);
            targetPos.y = playerPosOrigin.y;
            moveableObj.transform.position = targetPos;

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        MainSceneEvenetManager.SetPlayerMoveable(true);
    }

    private IEnumerator SetEularRoutine(Vector3 newEular)
    {
        float animTime = touchMoveDuration;
        float time = 0;

        Quaternion originRot = Quaternion.Euler(moveableObj.transform.eulerAngles);
        Quaternion destRot = Quaternion.Euler(newEular);
        Quaternion targetRot;

        Quaternion originCamRot = moveableCamera.transform.localRotation;

        MainSceneEvenetManager.SetPlayerMoveable(false);
        while (time < animTime)
        {
            targetRot = Quaternion.LerpUnclamped(originRot, destRot, time/animTime);

            moveableObj.transform.rotation = targetRot;

            moveableCamera.transform.localRotation = Quaternion.LerpUnclamped(originCamRot, Quaternion.identity, time / animTime);

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        MainSceneEvenetManager.SetPlayerMoveable(true);
    }
}
