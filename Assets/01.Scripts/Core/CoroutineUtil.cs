using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtil : MonoBehaviour
{
    private static GameObject _coroutineObj;
    private static CoroutineExecutor _coroutineExecutor;

    static CoroutineUtil()
    {
        _coroutineObj = new GameObject("CoroutineObj");
        _coroutineExecutor = _coroutineObj.AddComponent<CoroutineExecutor>();
    }

    public static void CallWaitForOneFrame(Action action) //1������ �ڿ� ����
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForOneFrame(action));
    }

    public static void CallWaitForSeconds(float seconds, Action afterAction = null) //n�� �ڿ� ����
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForSeconds(seconds, afterAction));
    }

    private static IEnumerator DoCallWaitForOneFrame(Action action)
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator DoCallWaitForSeconds(float seconds, Action afterAction)
    {
        yield return new WaitForSeconds(seconds);
        afterAction?.Invoke();
    }

    private class CoroutineExecutor : MonoBehaviour { }
}
