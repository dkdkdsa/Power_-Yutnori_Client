using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

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

    public static ScoreAndSpawnManager Instance { get; private set; }

    public int spawnCount { get; private set; } = 4;
    private PlayerType currentPlayerType;

    private void Awake()
    {
        
        Instance = this;

    }

    protected override void Start()
    {

        base.Start();

        currentPlayerType = (PlayerType)NetworkManager.Instance.ClientId - 1;

    }

    public void SpawnPlayer(PlayerType playerType)
    {

        if(currentPlayerType == playerType)
        {

            spawnCount--;

        }

    }

    public void CatchPlayer(PlayerType type, int count)
    {

        var p = new ScoreLinkParam() { score = count, playerType = type };

        LinkMethod(CatchPlayerLink, p);

    }

    public void CatchPlayerLink(ScoreLinkParam param)
    {

        if (param.playerType != currentPlayerType) return;

        spawnCount += param.score;

    }

}