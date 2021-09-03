﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WUC_TouchMove : MonoBehaviour
{
    [SerializeField]
    private Button touchBtn;
    private Vector3 targetPos;
    private Vector3 targetEular;

    private void Awake ()
    {
        touchBtn.onClick.AddListener(OnTouched);
    }

    private void OnTouched()
    {
        targetPos = transform.position;
        targetEular = transform.localEulerAngles;

        MainSceneEvenetManager.MoveWUC_Touch(targetPos, targetEular);
    }
}
