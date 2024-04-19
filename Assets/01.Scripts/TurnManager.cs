using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    private TurnType _curTurnType;
    public TurnType CurTurnType => _curTurnType;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;

        _curTurnType = TurnType.RedPlayerTurn; // 처음에 레드로 시작
    }

    public void ChangeTurn()
    {
        _curTurnType =
            _curTurnType == TurnType.RedPlayerTurn ? TurnType.BluePlayerTurn 
                                                     :TurnType.RedPlayerTurn; // 귀찮으니 삼항연산쓰겟음
        
    }
}
