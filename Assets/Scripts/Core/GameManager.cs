using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    // Booleans

    private bool isOnBgm;
    public bool IsOnBgm { get { return isOnBgm; } }

    private void Start ()
    {
        Init();
    }

    private void Init()
    {
        // StartBgm
        BGSoundManager.inst.ChangeClip(0);
        BGSoundManager.inst.BgmSoundOnOff(true);
        isOnBgm = true;

        // PlayerMovement
        SetPlayerMoveable(false);

        EventController.inst.OnBGMBtnClicked += ChangeBgmState;
        EventController.inst.OnIntroLastBtnClicked += GameStart;
        EventController.inst.OnExitBtnClicked += GameExit;
    }

    private void ChangeBgmState()
    {
        BGSoundManager.inst.BgmSoundOnOff(!IsOnBgm);
        isOnBgm = !isOnBgm;
    }

    private void GameStart()
    {
        SetPlayerMoveable(true);
    }

    private void GameExit ()
    {

    }

    #region PlayerMovement
    public void SetPlayerMoveable (bool isMoveable)
    {
        isPlayerMoveable = isMoveable;

        if (MobileCheck.isMobile())
        {
            if (EventController.inst.OnMobileMoveableChanged != null)
            {
                EventController.inst.OnMobileMoveableChanged.Invoke(isMoveable);
            }
        }
        else
        {
            if (EventController.inst.OnPCMoveableChanged != null)
            {
                EventController.inst.OnPCMoveableChanged.Invoke(isMoveable);
            }
        }
    }

    private bool isPlayerMoveable;
    public bool IsPlayerMoveable ()
    {
        return isPlayerMoveable;
    }

    public void MoveWUC_Touch (Vector3 newPos, Vector3 newEular)
    {
        if (EventController.inst.SetPlayerNewPos != null && Vector3.Distance(newPos, MoveManager.inst.moveableObj.transform.position) >= 0.01f )
        {
            EventController.inst.SetPlayerNewPos.Invoke(newPos);
        }

        if (EventController.inst.SetPlayerNewEular != null)
        {
            EventController.inst.SetPlayerNewEular.Invoke(newEular);
        }
    }
    #endregion

}
