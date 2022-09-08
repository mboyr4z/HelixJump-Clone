using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, IDestroyable, IFinishLine
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Finish(Action action)
    {
        action.Invoke();
    }
}
