using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseSpace : MonoBehaviour
{
    protected SpaceTypeEnum _spaceType;
    public SpaceTypeEnum SpaceType => _spaceType;

    private void Start()
    {
        SetUpSpaceType();
    }

    protected virtual void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.Normal;
    }

    private void OnMouseDown()
    {
        
    }
}
