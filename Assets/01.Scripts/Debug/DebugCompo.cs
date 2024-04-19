using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityNet;

public struct EX : INetSerializeable
{

    public int clientId;

    public void Deserialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        Serializer.Deserialize(ref clientId, ref buffer, ref count);

    }

    public void Serialize(ref ArraySegment<byte> buffer, ref ushort count)
    {

        clientId.Serialize(ref buffer, ref count);

    }

}

public class DebugCompo : NetBehavior
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            LinkMethod(Link, new EX { clientId = NetworkManager.Instance.ClientId});

        }

    }

    public void Link(EX p)
    {

        Debug.Log($"asdf : {p.clientId}");

    }

}
