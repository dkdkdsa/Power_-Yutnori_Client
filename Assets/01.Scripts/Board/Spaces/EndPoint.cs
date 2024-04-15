using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : BaseSpace
{
    protected override void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.EndPoint;
    }
}
