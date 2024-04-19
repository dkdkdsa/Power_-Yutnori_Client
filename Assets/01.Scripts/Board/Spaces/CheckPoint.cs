using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : BaseSpace
{
    [SerializeField]
    private MoveDirTypeEnum _checkPointMoveDirType;
    public MoveDirTypeEnum CheckPointMoveDirType => _checkPointMoveDirType;

    public override Vector2 GetShotCutDir()
    {
        Vector2 nextSpace = Vector2.zero;

        switch (_checkPointMoveDirType)
        {
            case MoveDirTypeEnum.Left:
                nextSpace = new Vector2(transform.position.x - MoveDistance,
                                        transform.position.y);
                break;
            case MoveDirTypeEnum.Right:
                nextSpace = new Vector2(transform.position.x + MoveDistance,
                                        transform.position.y);
                break;
            case MoveDirTypeEnum.Up:
                nextSpace = new Vector2(transform.position.x,
                                        transform.position.y + MoveDistance);
                break;
            case MoveDirTypeEnum.Down:
                nextSpace = new Vector2(transform.position.x,
                                        transform.position.y - MoveDistance);
                break;
            case MoveDirTypeEnum.RightDown:
                nextSpace = new Vector2(transform.position.x + MoveDistance,
                                        transform.position.y - MoveDistance);
                break;
            case MoveDirTypeEnum.LeftDown:
                nextSpace = new Vector2(transform.position.x - MoveDistance,
                                        transform.position.y - MoveDistance);
                break;
        }

        return nextSpace;
    }
}
