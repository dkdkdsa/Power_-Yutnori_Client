using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;



public struct CatchLinkParam : INetSerializeable
{

    public PlayerType playerType;
    public int score;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        int res = 0;
        Serializer.Deserialize(ref res, ref buffer, ref count);

        playerType = (PlayerType)res;

        Serializer.Deserialize(ref score, ref buffer, ref count);

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        ((int)playerType).Serialize(ref buffer, ref count);
        score.Serialize(ref buffer, ref count);

    }

}

public struct ScoreLinkParam : INetSerializeable
{
    public PlayerType playerType;
    public int score;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        int res = 0;
        Serializer.Deserialize(ref res, ref buffer, ref count);

        playerType = (PlayerType)res;

        Serializer.Deserialize(ref score, ref buffer, ref count);

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        ((int)playerType).Serialize(ref buffer, ref count);
        score.Serialize(ref buffer, ref count);

    }

}

public class ScoreAndSpawnManager : NetBehavior
{

    [SerializeField] private NetObject scoreAndSpawnUIPrefab;

    public static ScoreAndSpawnManager Instance { get; private set; }

    public int SpawnCount { get; private set; } = 4;
    public int Score { get; private set; }
    private PlayerType currentPlayerType;

    public event Action<PlayerType, int> OnCatchPlayer;
    public event Action<PlayerType, int> OnAddScore;

    private void Awake()
    {
        
        Instance = this;

    }

    protected override void Start()
    {

        base.Start();

        currentPlayerType = (PlayerType)NetworkManager.Instance.ClientId - 1;

        NetworkManager.Instance.SpawnNetObject(scoreAndSpawnUIPrefab.name, Vector3.zero, Quaternion.identity);

    }

    public void SpawnPlayer(PlayerType playerType)
    {

        if(currentPlayerType == playerType)
        {

            SpawnCount--;

        }

    }

    public void CatchPlayer(PlayerType type, int count)
    {

        var p = new CatchLinkParam() { score = count, playerType = type };

        LinkMethod(CatchPlayerLink, p);

    }

    public void AddScore(PlayerType type, int count)
    {

        var p = new ScoreLinkParam() { score = count, playerType = type };

        LinkMethod(AddScoreLink, p);

    }

    public void CatchPlayerLink(CatchLinkParam param)
    {

        OnCatchPlayer?.Invoke(param.playerType, param.score);

        if (param.playerType != currentPlayerType) return;

        SpawnCount += param.score;

    }

    public void AddScoreLink(ScoreLinkParam param)
    {

        OnAddScore?.Invoke(param.playerType, param.score);

        if (param.playerType != currentPlayerType) return;

        Score += param.score;

    }

}