using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : SingletonBehavior<EventController>
{
    // Player Movement
    public Action<Vector3> SetPlayerNewPos = null;
    public Action<Vector3> SetPlayerNewEular = null;

    public Action<bool> OnMobileMoveableChanged = null;
    public Action<bool> OnPCMoveableChanged = null;

    // UI Event
    public Action OnIntroLastBtnClicked = null;
    public Action OnBGMBtnClicked = null;
    public Action OnExitBtnClicked = null;

}
