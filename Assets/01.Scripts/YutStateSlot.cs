using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class YutStateSlot : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private TMP_Text text;

    private YutSystem system;

    public int curIdx { get; private set; }

    private void Awake()
    {
        
        system = FindObjectOfType<YutSystem>();

    }

    public void SetUp(YutState state, int idx)
    {

        text.text = GetYutText(state);
        curIdx = idx;

    }

    private string GetYutText(YutState state)
    {

        return state switch
        {
            YutState.Do => "��",
            YutState.Gay => "��",
            YutState.Girl => "��",
            YutState.Yut => "��",
            YutState.Mo => "��",
            _ => string.Empty,
        };

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (system.currentSelectIdx != -1 && TurnManager.Instance.MyTurn) return;

        text.color = Color.red;
        system.Select(curIdx);

    }
}
