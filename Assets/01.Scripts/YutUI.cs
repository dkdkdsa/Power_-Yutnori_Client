using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class YutUI : MonoBehaviour
{

    [SerializeField] private Sprite back, front, backCh;
    private List<Image> yutImages = new();
    private List<Yut> yuts = new();

    private void Awake()
    {
        
        int cnt = transform.childCount;


        for(int i = 0; i < cnt; i++)
        {

            var obj = transform.GetChild(i);

            yutImages.Add(obj.GetComponent<Image>());
            yuts.Add(obj.GetComponent<Yut>());

        }
        
    }

    public void SetUI(YutState state, int res)
    {

        var list = yutImages.ToList();

        Sequence seq = DOTween.Sequence();

        foreach(var item in yuts)
        {

            seq.Join(item.JumpAndRotate());

        }

        for(int i = 0; i < res; i++)
        {

            var image = list[Random.Range(0, list.Count)];
            image.sprite = front;

            list.Remove(image);

        }

        if(state == YutState.BackDo)
        {

            list[0].sprite = backCh;
            list.Clear();

        }

        foreach(var item in list)
        {

            item.sprite = back;

        }

        seq.AppendCallback(() => HandleAnimeComp(state));

    }

    private void HandleAnimeComp(YutState state)
    {

        FindObjectOfType<PlayersController>().PlayerMoveEventHandler((int)state);

    }

}
