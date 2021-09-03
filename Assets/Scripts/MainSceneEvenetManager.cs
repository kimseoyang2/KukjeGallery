using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneEvenetManager : MonoBehaviour
{
    public static Action<Vector3> SetPlayerNewPos;
    public static Action<Vector3> SetPlayerNewEular;

    public static Action<bool> OnMobileMoveableChanged;
    public static Action<bool> OnPCMoveableChanged;

    public static void SetPlayerMoveable (bool isMoveable)
    {
        isPlayerMoveable = isMoveable;

        if(MobileCheck.isMobile())
        {
            if(OnMobileMoveableChanged != null)
            {
                OnMobileMoveableChanged.Invoke(isMoveable);
            }
        }
        else
        {
            if(OnPCMoveableChanged != null)
            {
                OnPCMoveableChanged.Invoke(isMoveable);
            }
        } 
    }

    private static bool isPlayerMoveable;
    public static bool IsPlayerMoveable()
    {
        return isPlayerMoveable;
    }

    public static void MoveWUC_Touch(Vector3 newPos, Vector3 newEular)
    {
        if(SetPlayerNewPos != null)
        {
            SetPlayerNewPos.Invoke(newPos);
        }

        if(SetPlayerNewEular != null)
        {
            SetPlayerNewEular.Invoke(newEular);
        }
    }
}
