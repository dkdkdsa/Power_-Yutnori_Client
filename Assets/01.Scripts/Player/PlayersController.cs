using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityNet;

public enum PlayerType
{

    Red,
    Blue,

}

public class PlayersController : MonoBehaviour
{
    [SerializeField]
    private Player _redPlayerPrefab;
    [SerializeField]
    private Player _bluePlayerPrefab;

    [SerializeField]
    private Transform _startTrm;

    private List<Player> _redPlayers = new();
    private List<Player> _bluePlayers = new();

    private Dictionary<TurnType, List<Player>> _players = new();

    private TurnType _curTurnType => TurnManager.Instance.CurTurnType;

    private void Start()
    {

        //for(int i = 0; i < 3; i++)
        //{
        //    _redPlayers[i] = Instantiate(_redPlayerPrefab, _startTrm.position, Quaternion.identity);
        //    _bluePlayers[i] = Instantiate(_bluePlayerPrefab, _startTrm.position, Quaternion.identity);
        
        //}

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
            PlayerMoveEventHandler(1);
        }
    }

    public void PlayerMoveEventHandler(int stepCount)
    {
        SignalHub.OnPlayerMoveEvent?.Invoke(stepCount);
    }

    public void SpawnPlayer(PlayerType type)
    {

        switch (type)
        {
            case PlayerType.Red:
                _redPlayers.Add(
                    NetworkManager.Instance.SpawnNetObject(_redPlayerPrefab.name, _startTrm.position, Quaternion.identity)
                    .GetComponent<Player>());
                break;
            case PlayerType.Blue:
                _bluePlayers.Add(
                    NetworkManager.Instance.SpawnNetObject(_bluePlayerPrefab.name, _startTrm.position, Quaternion.identity)
                    .GetComponent<Player>());
                break;

        }

    }

    private void MovePlayer(int stepCount)
    {
        Player movePlayer = _players[_curTurnType]
                            .Where(p => p.IsPiecedOnBoard)
                            .FirstOrDefault(); // 일단은 걍 나와있는 놈들 중 첫번째 걸로 할게

        if (movePlayer == default)
        {
            movePlayer = _players[_curTurnType][0];
        }

        StartCoroutine(movePlayer.Move(stepCount));
    }



    private void OnDisable()
    {
        SignalHub.OnPlayerMoveEvent -= MovePlayer;
    }
}
