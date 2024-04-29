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

    private Player _selectPlayer;
    private bool IsSelectPlayer => _selectPlayer != null;

    private void OnEnable()
    {
        SignalHub.OnPlayerMoveEvent += MovePlayer;
    }

    public void PlayerMoveEventHandler(int stepCount, Action<bool> moveEndCallback = null)
    {
        SignalHub.OnPlayerMoveEvent?.Invoke(stepCount, moveEndCallback);
    }

    public void SpawnPlayer(PlayerType type)
    {

        ScoreAndSpawnManager.Instance.SpawnPlayer(type);

        switch (type)
        {
            case PlayerType.Red:
                Player redplayer = NetworkManager.Instance.SpawnNetObject(_redPlayerPrefab.name, _startTrm.position, Quaternion.identity, NetworkManager.Instance.ClientId)
                    .GetComponent<Player>();
                _redPlayers.Add(redplayer);
                redplayer.SelectPlayer();
                break;
            case PlayerType.Blue:
                Player blueplayer = NetworkManager.Instance.SpawnNetObject(_bluePlayerPrefab.name, _startTrm.position, Quaternion.identity, NetworkManager.Instance.ClientId)
                    .GetComponent<Player>();
                _bluePlayers.Add(blueplayer);
                blueplayer.SelectPlayer();
                break;

        }

    }

    private void MovePlayer(int stepCount, Action<bool> moveEndCallBack)
    {
        StartCoroutine(WaitUntilSelectPlayerCorou(stepCount, moveEndCallBack));
    }

    private IEnumerator WaitUntilSelectPlayerCorou(int stepCount, Action<bool> moveEndCallBack)
    {
        yield return new WaitUntil(() =>
        {
            return IsSelectPlayer;
        });

        StartCoroutine(_selectPlayer.Move(stepCount, moveEndCallBack));
        _selectPlayer = null;
    }

    public void SetPlayer(Player player)
    {
        _selectPlayer = player;
    }

    private void OnDisable()
    {
        SignalHub.OnPlayerMoveEvent -= MovePlayer;
    }
}
