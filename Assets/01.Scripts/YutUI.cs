using Core;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityNet;

public struct SetUpUI : INetSerializeable
{

    public YutState state;
    public int res;

    public void Deserialize(ref System.ArraySegment<byte> buffer, ref ushort count)
    {

        Serializer.Deserialize(ref res, ref buffer, ref count);
        int stateRes = 0;
        Serializer.Deserialize(ref stateRes, ref buffer, ref count);
        state = (YutState)stateRes;

    }

    public void Serialize(ref System.ArraySegment<byte> buffer, ref ushort count)
    {

        res.Serialize(ref buffer, ref count);
        ((int)state).Serialize(ref buffer, ref count);

    }
}

public class YutUI : NetBehavior
{

    [SerializeField] private Sprite back, front, backCh;
    private List<Image> yutImages = new();
    private List<Yut> yuts = new();
    private YutSystem system;

    private PlayersController _playersController;

    private void Awake()
    {
        
        int cnt = transform.childCount;
        system = FindObjectOfType<YutSystem>();
        system.SetUI(this);


        for(int i = 0; i < cnt; i++)
        {

            var obj = transform.GetChild(i);

            if (obj.TryGetComponent(out Yut yut))
            {
                yutImages.Add(obj.GetComponent<Image>());
                yuts.Add(obj.GetComponent<Yut>());
            }
        }

        _playersController = FindObjectOfType<PlayersController>();
    }

    public void SetUI(SetUpUI param)
    {

        var list = yutImages.ToList();

        Sequence seq = DOTween.Sequence();

        foreach(var item in yuts)
        {

            seq.Join(item.JumpAndRotate());

        }

        for(int i = 0; i < param.res; i++)
        {

            var image = list[Random.Range(0, list.Count)];
            image.sprite = front;

            list.Remove(image);

        }

        foreach(var item in list)
        {

            item.sprite = back;

        }

        seq.AppendCallback(() => HandleAnimeComp(param.state));

    }

    private void HandleAnimeComp(YutState state)
    {

        if (TurnManager.Instance.MyTurn)
        {
            //FindObjectOfType<PlayersController>().PlayerMoveEventHandler((int)state);
            system.OnAnimeEnd();

        }

        //gameObject.SetActive(false);

    }

    internal void SetUILink(YutState yutState, int res)
    {

        var setUp = new SetUpUI { state = yutState, res = res };
        LinkMethod(SetUI,setUp);

    }

}
