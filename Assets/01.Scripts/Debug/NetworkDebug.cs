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

            NetworkManager.Instance.Connected();

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            NetworkManager.Instance.SpawnNetObject("NetObj", Random.insideUnitCircle * 3, Quaternion.identity);

        }


    }

}
