using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    BaseSpace[] _childMoveSpaces;

    public BaseSpace[] ChildSpaces => _childMoveSpaces;

    public List<BaseSpace> asdasd = new List<BaseSpace>();

    private void Awake()
    {
        SetChildSpaces();
    }

    private void SetChildSpaces()
    {
        _childMoveSpaces = GetComponentsInChildren<BaseSpace>();

        for(int i = 0; i < _childMoveSpaces.Length; i++)
        {
            asdasd.Add(_childMoveSpaces[i]);
        }
    }
}
