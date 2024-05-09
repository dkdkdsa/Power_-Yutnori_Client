using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityNet;
using Random = UnityEngine.Random;

public enum YutState
{

    Do = 1,
    Gay = 2,
    Girl = 3,
    Yut = 4,
    Mo = 5,

}

public class YutSystem : MonoBehaviour
{

    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private GameObject oneMorePrefab;
    [SerializeField] private GameObject yutStateUIPrefab;

    private List<YutState> states = new();
    private YutUI yutUI;
    private YutStateUI yutStateUI;
    private PlayersController playerController;
    private bool throwAble = true;
    private int loopCnt;
    public int currentSelectIdx { get; private set; } = -1;

    public event Action<YutState> OnThrow;
    public event Action<YutState> OnMove;

    private void Start()
    {

        NetworkManager.Instance.OnNetworkConnected += HandleConnected;
        playerController = FindObjectOfType<PlayersController>();

    }

    private void HandleConnected()
    {

        if(NetworkManager.Instance.ClientId == 1)
        {

            NetworkManager.Instance.SpawnNetObject(uiPrefab.name, Vector3.zero, Quaternion.identity);
            NetworkManager.Instance.SpawnNetObject("ScoreManager", Vector3.zero, Quaternion.identity);
            NetworkManager.Instance.SpawnNetObject(yutStateUIPrefab.name, Vector3.zero, Quaternion.identity);

        }

    }

    public void SetUI(YutUI ui)
    {

        yutUI = ui;

    }

    public void SetUI(YutStateUI ui)
    {

        yutStateUI = ui;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && TurnManager.Instance.MyTurn && throwAble)
        {

            //FindObjectOfType<PlayersController>().SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);
            ThrowYut();

        }

    }

    private void ThrowYut()
    {

        throwAble = false;

        int res = 0;

        for (int i = 0; i < 4; i++)
        {

            res += Random.Range(0, 2);

        }

        loopCnt++;
        states.Add(GetYutState(res));

        yutUI.SetUILink(GetYutState(res), res);
        yutStateUI.ThrowYut(GetYutState(res), states.Count - 1);


    }

    public void OnAnimeEnd()
    {

        if(states.Last() == YutState.Mo || states.Last() == YutState.Yut)
        {

            NetworkManager.Instance.SpawnNetObject(oneMorePrefab.name, 
                Vector3.zero, Quaternion.identity, NetworkManager.Instance.ClientId);

            throwAble = true;

        }
        else
        {

            StartCoroutine(MoveCo());

        }

        SignalHub.OnSetEnableButtonEvent?.Invoke();
    }

    private IEnumerator MoveCo()
    {

        bool state = false;
        bool rethrow = false;
        throwAble = false;
        int cnt = loopCnt;
        for (int it = 0; it < cnt; it++)
        {


            yield return StartCoroutine(WaitSelectCo());
            var item = states[currentSelectIdx];
            loopCnt--;

            Player[] players = FindObjectsOfType<Player>()
                .Where(p => p.NetObject.IsOwner)
                .ToArray();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].SetSelectable();
            }

            Debug.Log(players.Length);

            //if (players.Length == 0)
            //{
            //    playerController.SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);
            //}

            playerController.PlayerMoveEventHandler((int)item, (x) =>
            {

                if (x == true)
                {

                    rethrow = true;

                }

                state = true;

            });


            yield return new WaitUntil(() => state);
            yutStateUI.Move(item, currentSelectIdx);
            currentSelectIdx = -1;

            if (rethrow)
            {

                NetworkManager.Instance.SpawnNetObject(oneMorePrefab.name,
                    Vector3.zero, Quaternion.identity, NetworkManager.Instance.ClientId);

                throwAble = true;

                yield break;

            }

            yield return new WaitForSeconds(0.5f);

            state = false;

        }

        loopCnt = 0;
        throwAble = true;
        states.Clear();

        TurnManager.Instance.ChangeTurn();

    }

    public void Select(int idx)
    {

        currentSelectIdx = idx;

    }

    private IEnumerator WaitSelectCo()
    {

        yield return new WaitUntil(() =>  currentSelectIdx != -1);

    }

    private YutState GetYutState(int total)
    {

        return total switch
        {

            0 => YutState.Yut,
            1 => YutState.Girl,
            2 => YutState.Gay,
            3 => DoOrBackDo(),
            4 => YutState.Mo,
            _ => YutState.Do
        };

    }

    private YutState DoOrBackDo()
    {

        if(Random.value > 0.4)
        {

            return YutState.Do;

        }

        return YutState.Do;

    }

}