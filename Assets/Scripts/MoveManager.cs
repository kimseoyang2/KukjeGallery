using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardMove), typeof(MouseDragRotate), typeof(TouchDragRotate))]
public class MoveManager : SingletonBehavior<MoveManager>
{
    [SerializeField]
    private KeyboardMove keyboardMove = null;
    [SerializeField]
    private MouseDragRotate mouseDragRotate = null;
    [SerializeField]
    private TouchDragRotate touchDragRotate = null;
    [SerializeField]
    private RayCastMove rayCastMove = null;

    [SerializeField]
    private Joystick joystickL = null;


    public GameObject moveableObj = null;

    public GameObject moveableCamera = null;

    [SerializeField, Range(0, 1)]
    private float moveSpeed = 0.5f;
    [SerializeField, Range(0, 1)]
    private float rotateSpeed = 0.5f;

    [SerializeField, Range(0.1f, 2f)]
    public float touchMoveDuration = 1f;

    private Coroutine lookPicCoroutine = null;
    private Coroutine resetCamCoroutine = null;

    protected override void Awake ()
    {
        base.Awake();

        EventController.inst.OnPCMoveableChanged += keyboardMove.SetMoveable;
        EventController.inst.OnPCMoveableChanged += mouseDragRotate.SetMouseDragable;
        EventController.inst.OnPCMoveableChanged += rayCastMove.SetMoveable;

        EventController.inst.OnMobileMoveableChanged += touchDragRotate.SetTouchDragable;
        EventController.inst.OnMobileMoveableChanged += joystickL.SetJoysticVisible;

        if (!MobileCheck.isMobile())
        {
            keyboardMove.OnMoveKeyClicked += Move;
            mouseDragRotate.OnDraged += Rotate;

            joystickL.SetJoysticVisible(false);

            EventController.inst.OnPCMoveableChanged.Invoke(true);
            EventController.inst.OnMobileMoveableChanged.Invoke(false);
        }
        else
        {
            touchDragRotate.OnTouchDrag += TouchRotate;

            joystickL.OnJoysticMove += Move;
            joystickL.OnJoysticRotate += Rotate;

            joystickL.SetJoysticVisible(true);

            EventController.inst.OnPCMoveableChanged.Invoke(false);
            EventController.inst.OnMobileMoveableChanged.Invoke(true);
        }



        EventController.inst.SetPlayerNewPos += SetPos;
        EventController.inst.SetPlayerNewEular += SetEular;
    }

    private void Start ()
    {

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

    public void SetPos (Vector3 newPos)
    {
        StartCoroutine(SetPosRoutine(newPos));
    }

    public void SetEular (Vector3 newEular)
    {
        StartCoroutine(SetEularRoutine(newEular));
    }

    public void LookPic (PictureViewPoint pic)
    {
        Vector3 newPos = Vector3.zero;
        newPos.y = pic.gameObject.transform.position.y;

        if (lookPicCoroutine != null)
        {
            StopCoroutine(lookPicCoroutine);
            lookPicCoroutine = null;
        }

        if (moveableCamera.transform.position != newPos && lookPicCoroutine == null)
        {
            lookPicCoroutine = StartCoroutine(LookPicRoutine(newPos));
        }
    }

    private IEnumerator LookPicRoutine (Vector3 newPos)
    {
        float time = 0;
        float duration = 0.5f;

        Vector3 fromPos = moveableCamera.transform.localPosition;
        Vector3 targetPos = newPos - new Vector3(0, moveableObj.transform.position.y + 1, 0);
        Vector3 curPos;

        while (time <= duration)
        {
            curPos = Vector3.Lerp(fromPos, targetPos, time / duration);
            moveableCamera.transform.localPosition = curPos;

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        moveableCamera.transform.localPosition = targetPos;

        lookPicCoroutine = null;
    }

    public void ResetCam()
    {
        if (moveableCamera.transform.localPosition != new Vector3(0, 2, 0) && resetCamCoroutine == null)
        {
            resetCamCoroutine = StartCoroutine(ResetCamPosRoutine());
        }
    }

    private IEnumerator ResetCamPosRoutine()
    {
        float time = 0;
        float duration = 0.5f;
        float originY = moveableCamera.transform.position.y;
        float curY;

        while(time <= duration)
        {
            curY = Mathf.Lerp(originY, 2, time / duration);
            moveableCamera.transform.localPosition = new Vector3(0, curY, 0);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        moveableCamera.transform.localPosition = new Vector3(0, 2, 0);
        resetCamCoroutine = null;
    }

    public void SetMovable (bool moveable)
    {
        if (!MobileCheck.isMobile())
        {
            EventController.inst.OnPCMoveableChanged.Invoke(true);
            EventController.inst.OnMobileMoveableChanged.Invoke(false);
        }
        else
        {
            EventController.inst.OnPCMoveableChanged.Invoke(false);
            EventController.inst.OnMobileMoveableChanged.Invoke(true);
        }
    }

    private IEnumerator SetPosRoutine (Vector3 newPos)
    {
        float animTime = touchMoveDuration;
        float time = 0;

        Vector3 playerPosOrigin = moveableObj.transform.position;
        Vector3 targetPos;

        GameManager.inst.SetPlayerMoveable(false);
        moveableObj.GetComponent<Rigidbody>().isKinematic = true;
        while (time < animTime)
        {
            targetPos = Vector3.Lerp(playerPosOrigin, newPos, time / animTime);
            targetPos.y = playerPosOrigin.y;
            moveableObj.transform.position = targetPos;

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        moveableObj.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.inst.SetPlayerMoveable(true);
    }

    private IEnumerator SetEularRoutine (Vector3 newEular)
    {
        float animTime = touchMoveDuration;
        float time = 0;

        Quaternion originRot = Quaternion.Euler(moveableObj.transform.eulerAngles);
        Quaternion destRot = Quaternion.Euler(newEular);
        Quaternion targetRot;

        Quaternion originCamRot = moveableCamera.transform.localRotation;

        GameManager.inst.SetPlayerMoveable(false);
        while (time < animTime)
        {
            targetRot = Quaternion.LerpUnclamped(originRot, destRot, time / animTime);

            moveableObj.transform.rotation = targetRot;

            moveableCamera.transform.localRotation = Quaternion.LerpUnclamped(originCamRot, Quaternion.identity, time / animTime);

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        GameManager.inst.SetPlayerMoveable(true);
    }
}
