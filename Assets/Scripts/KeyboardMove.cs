using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMove : MonoBehaviour
{
    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;
    private KeyCode right = KeyCode.D;
    private KeyCode left = KeyCode.A;

    private Coroutine moveUpdateCoroutine = null;

    public Action<Vector2> OnMoveKeyClicked = null;

    private const float moveMulti = 1.5f;

    //private void Awake ()
    //{
    //    MainSceneEvenetManager.OnMoveableChanged += SetMoveable;
    //}

    public void SetMoveable (bool isMoveable)
    {
        if (isMoveable)
        {
            if (moveUpdateCoroutine == null)
            {
                moveUpdateCoroutine = StartCoroutine(MoveUpdateRoutine());
            }
        }
        else
        {
            if (moveUpdateCoroutine != null)
            {
                StopCoroutine(moveUpdateCoroutine);
                moveUpdateCoroutine = null;
            }
        }
    }

    private IEnumerator MoveUpdateRoutine ()
    {
        while (true)
        {
            Vector2 additionalPos = Vector2.zero;

            if (Input.GetKey(forward))
            {
                additionalPos += Vector2.up;
            }

            if (Input.GetKey(backward))
            {
                additionalPos += Vector2.down;
            }

            if (Input.GetKey(right))
            {
                additionalPos += Vector2.right;
            }

            if (Input.GetKey(left))
            {
                additionalPos += Vector2.left;
            }

            if(OnMoveKeyClicked != null)
            {
                OnMoveKeyClicked.Invoke(additionalPos * moveMulti);
            }

            yield return null;
        }
    }


}
