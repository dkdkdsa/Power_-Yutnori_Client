using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private BaseSpace[] _childSpaces;
    public BaseSpace[] Spaces => _childSpaces;

    private void Awake()
    {
        SetChildSpaces();
    }

    private void SetChildSpaces()
    {
        _childSpaces = GetComponentsInChildren<BaseSpace>();
    }
}
