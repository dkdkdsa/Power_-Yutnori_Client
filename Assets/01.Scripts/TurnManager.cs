using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public class TurnManager : MonoBehaviour
{

    [SerializeField] private GameObject yutUI;
    public static TurnManager Instance;

    private TurnType _curTurnType;
    public TurnType CurTurnType => _curTurnType;
    public bool MyTurn 
    { 

        get
        {

            return NetworkManager.Instance.ClientId - 1 == (int)_curTurnType;

        } 

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;

        _curTurnType = TurnType.RedPlayerTurn; // 처음에 레드로 시작

    }

    private void Start()
    {

        NetworkManager.Instance.OnTurnChangeEvent += HandleTurnChanged;

    }

    private void HandleTurnChanged(int obj)
    {

        _curTurnType = (TurnType)obj;

    }

    public void ChangeTurn()
    {

        var p = new TurnChangePacket();

        NetworkManager.Instance.SendPacket(p);

        //yutUI.gameObject.SetActive(true);
        
    }
}
