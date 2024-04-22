using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityNet;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum YutState
{

    Do = 1,
    Gay = 2,
    Girl = 3,
    Yut = 4,
    Mo = 6,

}

public class YutSystem : MonoBehaviour
{

    [SerializeField] private GameObject uiPrefab;

    private List<YutState> states = new();
    private YutUI yutUI;
    private PlayersController playerController;

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

        }

        yutUI = FindObjectOfType<YutUI>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && TurnManager.Instance.MyTurn)
        {

            //FindObjectOfType<PlayersController>().SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);
            ThrowYut();

        }

    }

    private void ThrowYut()
    {

        int res = 0;

        for (int i = 0; i < 4; i++)
        {

            res += Random.Range(0, 2);

        }

        states.Add(GetYutState(res));

        yutUI.SetUILink(GetYutState(res), res);

    }

    public void OnAnimeEnd()
    {

        if(states.Last() == YutState.Mo || states.Last() == YutState.Yut)
        {

            Debug.Log("�ѹ� ��");

        }
        else
        {

            StartCoroutine(MoveCo());

        }

    }

    private IEnumerator MoveCo()
    {

        bool state = false;
        foreach(var item in states)
        {
            Player[] players = FindObjectsOfType<Player>()
                .Where(p => p.NetObject.IsOwner)
                .ToArray();

            for(int i = 0; i < players.Length; i++)
            {
                players[i].SetCanSelect();
            }

            if (players.Length == 0)
            {
                playerController.SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);
            }

            playerController.PlayerMoveEventHandler((int)item, (x) => state = true);

            yield return new WaitUntil(() => state);

            state = false;

        }

        states.Clear();

        //TurnManager.Instance.ChangeTurn();

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