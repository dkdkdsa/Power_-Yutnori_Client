using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityNet;

[System.Serializable]
public class ScoreUIType
{

    public TMP_Text text;
    public PlayerType playerType;
    public int current;

}

public class ScoreUIController : NetBehavior
{

    [SerializeField] private List<ScoreUIType> addPlayerText;

    private void Awake()
    {

        ScoreAndSpawnManager.Instance.OnAddScore += HandleAddScore;

    }


    private void HandleAddScore(PlayerType type, int arg2)
    {

        var obj = addPlayerText.Find(x => x.playerType == type);
        obj.current += arg2;
        obj.text.text = obj.current.ToString();

    }

}