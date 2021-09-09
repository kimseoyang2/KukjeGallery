using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T inst = null;

    protected virtual void Awake ()
    {
        if(inst == null)
        {
            inst = this as T;
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }
}
