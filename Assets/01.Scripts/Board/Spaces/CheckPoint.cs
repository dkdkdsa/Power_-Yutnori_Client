using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : BaseSpace
{
    protected override void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.CheckPoint;
    }
}
