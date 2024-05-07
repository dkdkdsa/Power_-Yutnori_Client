using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityNet;
using DG.Tweening;

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
    [SerializeField] private TMP_Text throwTextPrefab;
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

        var obj = Instantiate(throwTextPrefab, Vector3.zero, Quaternion.identity);
        obj.text = GetYutText(yutState);

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.3f);
        seq.Append(obj.DOFade(0, 1.5f));
        seq.AppendCallback(() => Destroy(obj.gameObject));

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
        //?
        Destroy(obj.gameObject);

    }

    private string GetYutText(YutState state)
    {

        return state switch
        {
            YutState.Do => "µµ",
            YutState.Gay => "°³",
            YutState.Girl => "°É",
            YutState.Yut => "À·",
            YutState.Mo => "¸ð",
            _ => string.Empty,
        };

    }

}
