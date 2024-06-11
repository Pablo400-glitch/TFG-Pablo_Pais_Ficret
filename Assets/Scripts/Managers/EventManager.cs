using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PointsUIUpdateEvent(int currentPoints, int maxPoints);
    public static event PointsUIUpdateEvent onPointsUIUpdateEvent;

    public delegate void FinishedGameEv(bool finished);
    public static event FinishedGameEv onFinishedGameEvent;

    public delegate void StartGameEv(bool start);
    public static event StartGameEv onStartGameEvent;

    public static void PointsUIUpdate(int currentPoints, int maxPoints)
    {
        if (onPointsUIUpdateEvent != null)
            onPointsUIUpdateEvent(currentPoints, maxPoints);
    }

    public static void FinishedGameEvent(bool finished)
    {
        if (onFinishedGameEvent != null)
            onFinishedGameEvent(finished);
    }

    public static void StartGameEvent(bool start)
    {
        if(onStartGameEvent != null)
            onStartGameEvent(start);
    }
}
