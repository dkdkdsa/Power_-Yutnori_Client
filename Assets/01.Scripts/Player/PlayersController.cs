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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMoveEventHandler(1);
        }
    }

    public void PlayerMoveEventHandler(int stepCount, Action<bool> moveEndCallback = null)
    {
        SignalHub.OnPlayerMoveEvent?.Invoke(stepCount, moveEndCallback);
    }

    public void SpawnPlayer(PlayerType type)
    {

        switch (type)
        {
            case PlayerType.Red:
                Player redplayer = NetworkManager.Instance.SpawnNetObject(_redPlayerPrefab.name, _startTrm.position, Quaternion.identity, NetworkManager.Instance.ClientId)
                    .GetComponent<Player>();
                _redPlayers.Add(redplayer);
                redplayer.SetCanSelect();
                break;
            case PlayerType.Blue:
                Player blueplayer = NetworkManager.Instance.SpawnNetObject(_bluePlayerPrefab.name, _startTrm.position, Quaternion.identity, NetworkManager.Instance.ClientId)
                    .GetComponent<Player>();
                _bluePlayers.Add(blueplayer);
                blueplayer.SetCanSelect();
                break;

        }

    }

    private void MovePlayer(int stepCount, Action<bool> moveEndCallBack)
    {
        //Player movePlayer = _players[_curTurnType]
        //                    .Where(p => p.IsPiecedOnBoard)
        //                    .FirstOrDefault(); // 일단은 걍 나와있는 놈들 중 첫번째 걸로 할게

        StartCoroutine(WaitUntilSelectPlayerCorou(stepCount));
    }

    private IEnumerator WaitUntilSelectPlayerCorou(int stepCount)
    {
        while (!IsSelectPlayer)
        {
            Debug.Log("Waiting Select Player..");
            yield return 0.1f;
        }
        Debug.Log("?!");
        //if (movePlayer == default)
        //{
        //    movePlayer = _players[_curTurnType][0];
        //}

        StartCoroutine(_selectPlayer.Move(stepCount));
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
