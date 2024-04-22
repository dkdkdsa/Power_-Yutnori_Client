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

    private void Update()
    {
        //if (!NetObject.IsOwner) return;
    }

    protected override void Start()
    {
        base.Start();
    }

    public IEnumerator Move(int stepCount)
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
                Debug.Log("골");
                isArrived = true;
                isPiecedOnBoard = false;
                NetObject.Despawn();
                yield break;
            }

            transform.DOMove(nextDir, moveSpeed);
            yield return new WaitForSeconds(movedelay);
        }

        Collider2D collider = Physics2D.Raycast(transform.position, transform.forward, 10f, _playerLayer).collider;
        // 적을 잡았을때
        if (collider != null)
        {
            if (collider.CompareTag($"{_playerType}"))
            {
                // 업었다.

            }
            else
            {
                // 잡았다.

            }
        }
        TurnManager.Instance.ChangeTurn();
    }
}
