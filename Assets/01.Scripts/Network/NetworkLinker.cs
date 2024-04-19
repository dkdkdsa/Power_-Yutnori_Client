using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNet;
using Core;
using System;

/// <summary>
/// 어셈블리 끼리의 연결점을 제공
/// </summary>
public class NetworkLinker
{

    private static NetworkLinker instance;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {

        instance = new();

        LocalNetworkEvent.MethodLinkEvent += instance.HandleLinked;

    }

    private void HandleLinked(MethodLinkPacketParam p)
    {

        var t = Type.GetType(p.typeName);

        var obj = p.saver.Casting(t);

        NetworkManager.Instance.LinkMethodInvoke(p.methodName, p.componentName, p.objectHash, obj);

    }
}
