using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UC_BaseComponent : MonoBehaviour
{
    [HideInInspector]
    public UP_BasePage parentPage;

    public abstract void BindDelegates ();
}
