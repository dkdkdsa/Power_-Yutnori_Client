using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;

public class DebugCompo : NetBehavior
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            LinkMethod(Link);

        }

    }

    public void Link()
    {

        Debug.Log("asdf");

    }

}
