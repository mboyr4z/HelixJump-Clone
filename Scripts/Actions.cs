using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions : MonoBehaviour
{
    public static Action act_win;

    public static Action act_lose;

    public static Action act_bounce;

    public static Action act_TouchedScoreTrigger;

    public static Action act_newRecord;

    public static Action act_replay;

    public static Action act_nextLevel;

    public static Action act_PointChanged;

    public static void InvokeAction(Action action)
    {
        action?.Invoke();
    }

}
