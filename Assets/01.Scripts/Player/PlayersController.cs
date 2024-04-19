using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    [SerializeField]
    private Player _redPlayerPrefab;
    [SerializeField]
    private Player _bluePlayerPrefab;

    [SerializeField]
    private Transform _startTrm;

    private Player[] _redPlayers = new Player[3];
    private Player[] _bluePlayers = new Player[3];

    private Dictionary<TurnType, Player[]> _players = new();

    private TurnType _curTurnType => TurnManager.Instance.CurTurnType;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            _redPlayers[i] = Instantiate(_redPlayerPrefab, _startTrm.position, Quaternion.identity);
            _bluePlayers[i] = Instantiate(_bluePlayerPrefab, _startTrm.position, Quaternion.identity);
        
        }

        _players.Add(TurnType.RedPlayerTurn, _redPlayers);
        _players.Add(TurnType.BluePlayerTurn, _bluePlayers);
    }

    private void OnEnable()
    {
        SignalHub.OnPlayerMoveEvent += MovePlayer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMoveEventHandler();
        }
    }

    public void PlayerMoveEventHandler()
    {
        SignalHub.OnPlayerMoveEvent?.Invoke();
    }

    private void MovePlayer()
    {
        Player movePlayer = _players[_curTurnType]
                            .Where(p => p.IsPiecedOnBoard)
                            .FirstOrDefault(); // 일단은 걍 나와있는 놈들 중 첫번째 걸로 할게

        if (movePlayer == default)
        {
            movePlayer = _players[_curTurnType][0];
        }

        StartCoroutine(movePlayer.Move(1));
    }



    private void OnDisable()
    {
        SignalHub.OnPlayerMoveEvent -= MovePlayer;
    }
}
