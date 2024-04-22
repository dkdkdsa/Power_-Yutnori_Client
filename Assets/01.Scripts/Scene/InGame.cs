using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public class InGame : MonoBehaviour
{

    private void Start()
    {
        
        NetworkManager.Instance.Connect();

    }

}
