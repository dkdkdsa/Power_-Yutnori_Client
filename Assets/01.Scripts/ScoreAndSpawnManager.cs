using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

public struct WinLinkParam : INetSerializeable
{
    public PlayerType playerType;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        int res = 0;
        Serializer.Deserialize(ref res, ref buffer, ref count);

        playerType = (PlayerType)res;

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        ((int)playerType).Serialize(ref buffer, ref count);

    }

}

public struct SpawnPlayerParam : INetSerializeable
{

    public PlayerType playerType;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        int res = 0;
        Serializer.Deserialize(ref res, ref buffer, ref count);

        playerType = (PlayerType)res;

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        ((int)playerType).Serialize(ref buffer, ref count);

    }

}

public class ScoreAndSpawnManager : NetBehavior
{

    [SerializeField] private NetObject scoreAndSpawnUIPrefab;
    [SerializeField] private TMP_Text winTextPrefab;

    public static ScoreAndSpawnManager Instance { get; private set; }

    public int SpawnCount { get; private set; } = 4;
    public int Score { get; private set; }
    private PlayerType currentPlayerType;

    public event Action<PlayerType, int> OnAddScore;
    public event Action<PlayerType, int> OnPlayerCatch;
    public event Action<PlayerType> OnSpawnPlayer;

    private bool isSpawnButtonClick;
    public bool IsSpawnButtonClick => isSpawnButtonClick;

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

        var p = new SpawnPlayerParam { playerType = playerType };

        LinkMethod(SpawnPlayerLink, p);

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

    public void SpawnPlayerLink(SpawnPlayerParam param)
    {

        OnSpawnPlayer?.Invoke(param.playerType);

    }

    public void CatchPlayerLink(CatchLinkParam param)
    {

        OnPlayerCatch?.Invoke(param.playerType, param.score);

        if (param.playerType != currentPlayerType) return;

        SpawnCount += param.score;

    }

    public void AddScoreLink(ScoreLinkParam param)
    {

        OnAddScore?.Invoke(param.playerType, param.score);

        if (param.playerType != currentPlayerType) return;

        Score += param.score;

        if(Score == 3)
        {

            var p = new WinLinkParam { playerType = currentPlayerType };

            LinkMethod(WinLink, p);

        }

    }

    public void WinLink(WinLinkParam p)
    {

        NetworkManager.Instance.Disconnect();

        Instantiate(winTextPrefab, Vector3.zero, Quaternion.identity).text 
            = $"WIN : {p.playerType.ToString()}";

    }

    public void SpawnButtonClick()
    {
        isSpawnButtonClick = true;
        CoroutineUtil.CallWaitForSeconds(1, () => isSpawnButtonClick = false);
    }
}