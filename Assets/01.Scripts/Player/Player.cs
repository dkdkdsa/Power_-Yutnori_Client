using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Playables;
using UnityNet;

public class Player : NetBehavior
{
    [SerializeField]
    private int tempStepCount;

    private bool isPiecedOnBoard = false;
    public bool IsPiecedOnBoard => isPiecedOnBoard;

    private bool isArrived = false;

    [SerializeField]
    private float moveSpeed = 0.2f;
    [SerializeField]
    private float movedelay = 0.3f;

    [SerializeField]
    private PlayerType _playerType;

    [SerializeField]
    private LayerMask _playerLayer;

    private PlayersController _playersController;

    private bool canSelect;

    private void Update()
    {
        //if (!NetObject.IsOwner) return;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        _playersController = FindObjectOfType<PlayersController>();
    }

    public IEnumerator Move(int stepCount, Action<bool> moveEndCallBack)
    {
        if(!isPiecedOnBoard)
        {
            isPiecedOnBoard = true;
            Vector2 nextDir = new Vector2(transform.position.x,
                                        transform.position.y + 1.5f);
            transform.DOMove(nextDir, moveSpeed);
            stepCount--;
            yield return new WaitForSeconds(movedelay);
        }

        for (int i = 0; i < stepCount; i++)
        {
            Vector2 nextDir = BoardManager.Instance.GetDirFromPlayerPos(transform.position, i);

            if(nextDir == Vector2.zero)
            {
                Debug.Log("ê³?);
                isArrived = true;
                isPiecedOnBoard = false;
                NetObject.Despawn();
                moveEndCallBack?.Invoke(true);
                yield break;
            }

            transform.DOMove(nextDir, moveSpeed);
            yield return new WaitForSeconds(movedelay);
        }

        //TurnManager.Instance.ChangeTurn();
        moveEndCallBack?.Invoke(false);
    }

    public void SetCanSelect()
    {
        Debug.Log("CanSelect");
        canSelect = true;
    }

    private void OnMouseDown()
    {
        if (NetObject.IsOwner && canSelect)
        {
            _playersController.SetPlayer(this);
        }
    }
}
