using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerMove(int stepCount);

public static class SignalHub
{
    public static PlayerMove OnPlayerMoveEvent;
}
