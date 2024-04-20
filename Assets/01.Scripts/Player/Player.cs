using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        //if (!NetObject.IsOwner) return;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        _spriteRenderer.color = new Color(255, 255, 255, 0.0f);
    }

    public IEnumerator Move(int stepCount)
    {
        if(!isPiecedOnBoard)
        {
            _spriteRenderer.color = Color.white;
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
                Debug.Log("움직");
                isArrived = true;
                isPiecedOnBoard = false;
                NetObject.Despawn();
            }

            transform.DOMove(nextDir, moveSpeed);
            yield return new WaitForSeconds(movedelay);
        }

        TurnManager.Instance.ChangeTurn();
    }
}
