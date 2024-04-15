using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : BaseSpace
{
    [SerializeField]
    private MoveDirTypeEnum _checkPointMoveDirType;
    public MoveDirTypeEnum CheckPointMoveDirType => _checkPointMoveDirType;

    protected override void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.CheckPoint;
    }
}
