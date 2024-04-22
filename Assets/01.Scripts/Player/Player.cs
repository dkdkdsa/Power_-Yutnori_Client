using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
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

        NetworkManager.Instance.OnTurnChangeEvent += SetSelectDisable;
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
                Debug.Log("rhf");
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

    public void SetSelectable()
    {
        canSelect = true;
    }

    public void SetSelectDisable(int obj)
    {
        canSelect = false;
    }

    public void SelectPlayer()
    {
        _playersController.SetPlayer(this);
    }

    private void OnMouseDown()
    {
        if (NetObject.IsOwner && canSelect)
        {
            SelectPlayer();
        }
        Debug.Log($"IsOwner: {NetObject.IsOwner}");
        Debug.Log($"canSelect: {canSelect}");
    }

    private void OnEnable()
    {
        NetworkManager.Instance.OnTurnChangeEvent -= SetSelectDisable;
    }
}
