using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public class ScoreUIController : NetBehavior
{

    private void Awake()
    {

        ScoreAndSpawnManager.Instance.OnAddScore += HandleAddScore;
        ScoreAndSpawnManager.Instance.OnCatchPlayer += HandleCatchPlayer;

    }

    private void HandleCatchPlayer(PlayerType type, int arg2)
    {



    }

    private void HandleAddScore(PlayerType type, int arg2)
    {



    }

}