using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public enum YutState
{

    Do = 1,
    Gay = 2,
    Girl = 3,
    Yut = 4,
    Mo = 6,
    BackDo = -1,

}

public class YutSystem : MonoBehaviour
{

    [SerializeField] private GameObject uiPrefab;

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

        Debug.Log(GetYutState(res).ToString());

        //나중에 바꾸기


        FindObjectOfType<YutUI>().SetUILink(GetYutState(res), res);

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

        return YutState.BackDo;

    }

}