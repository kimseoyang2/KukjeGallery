using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UP_BasePage : MonoBehaviour
{

    protected virtual void Awake()
    {
        foreach(Transform child in transform)
        {
            UC_BaseComponent childCompo = child.GetComponent<UC_BaseComponent>();
            if(childCompo)
            {
                childCompo.parentPage = this;
                childCompo.BindDelegates();
            }
        }
            
        Init();
    }

    public abstract void Init ();
}
