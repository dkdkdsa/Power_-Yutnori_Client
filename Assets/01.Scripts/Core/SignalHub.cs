using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerMove();

public static class SignalHub
{
    public static PlayerMove OnPlayerMoveEvent;
}
