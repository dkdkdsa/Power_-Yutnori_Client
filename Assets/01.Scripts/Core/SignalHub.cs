using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerMove(int stepCount, Action<bool> moveEndCallback);
public delegate void SetEnableButton();

public static class SignalHub
{
    public static PlayerMove OnPlayerMoveEvent;
    public static SetEnableButton OnSetEnableButtonEvent;
}
