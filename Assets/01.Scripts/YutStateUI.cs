using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityNet;

public struct YutStateLinkParam : INetSerializeable
{
    public YutState yutState;
    public int idx;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        int res = 0;
        Serializer.Deserialize(ref res, ref buffer, ref count);
        yutState = (YutState)res;

        Serializer.Deserialize(ref idx, ref buffer, ref count);

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        ((int)yutState).Serialize(ref buffer, ref count);
        idx.Serialize(ref buffer, ref count);

    }
}

public class YutStateUI : NetBehavior
{

    [SerializeField] private YutStateSlot yutStateText;
    [SerializeField] private Transform yutStateTrm;

    private List<YutStateSlot> slots = new();

    private void Awake()
    {

        var system = FindObjectOfType<YutSystem>();
        system.SetUI(this);

    }

    public void ThrowYut(YutState yutState, int idx)
    {

        LinkMethod(ThrowYutLink, new YutStateLinkParam { yutState = yutState, idx = idx });

    }

    public void ThrowYutLink(YutStateLinkParam param)
    {

        var item = Instantiate(yutStateText, yutStateTrm);
        item.SetUp(param.yutState, param.idx);
        slots.Add(item);

    }

    public void Move(YutState yutState, int idx)
    {

        LinkMethod(MoveLink, new YutStateLinkParam { yutState = yutState, idx = idx });

    }

    public void MoveLink(YutStateLinkParam param)
    {

        var obj = slots.Find(x => x.curIdx == param.idx);
        slots.Remove(obj);
        Destroy(obj.gameObject);

    }

}
