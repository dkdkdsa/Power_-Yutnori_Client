using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public class NetworkDebug : MonoBehaviour
{

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {

            NetworkManager.Instance.Connect();

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            NetworkManager.Instance.SpawnNetObject("Player_Debug", new Vector2(3.744f, -3.69f), Quaternion.identity, NetworkManager.Instance.ClientId);

        }


    }

}
