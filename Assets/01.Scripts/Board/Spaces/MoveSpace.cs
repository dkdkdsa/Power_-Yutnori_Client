using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpace : BaseSpace
{
    protected override void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.Normal;
    }
}
