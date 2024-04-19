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

        _curTurnType = TurnType.RedPlayerTurn; // ó���� ����� ����
    }

    public void ChangeTurn()
    {
        _curTurnType =
            _curTurnType == TurnType.RedPlayerTurn ? TurnType.BluePlayerTurn 
                                                     :TurnType.RedPlayerTurn; // �������� ���׿��꾲����
        
    }
}
