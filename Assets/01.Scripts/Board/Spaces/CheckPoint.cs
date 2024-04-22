using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : BaseSpace
{
    [SerializeField]
    private MoveDirTypeEnum _checkPointMoveDirType;
    public MoveDirTypeEnum CheckPointMoveDirType => _checkPointMoveDirType;

    [SerializeField]
    private float shortcutMoveDistance;

    public override Vector2 GetShotCutDir()
    {
        Vector2 nextSpace = Vector2.zero;

        switch (_checkPointMoveDirType)
        {
            case MoveDirTypeEnum.Left:
                nextSpace = new Vector2(transform.position.x - shortcutMoveDistance,
                                        transform.position.y);
                break;
            case MoveDirTypeEnum.Right:
                nextSpace = new Vector2(transform.position.x + shortcutMoveDistance,
                                        transform.position.y);
                break;
            case MoveDirTypeEnum.Up:
                nextSpace = new Vector2(transform.position.x,
                                        transform.position.y + shortcutMoveDistance);
                break;
            case MoveDirTypeEnum.Down:
                nextSpace = new Vector2(transform.position.x,
                                        transform.position.y - shortcutMoveDistance);
                break;
            case MoveDirTypeEnum.RightDown:
                nextSpace = new Vector2(transform.position.x + shortcutMoveDistance,
                                        transform.position.y - shortcutMoveDistance);
                break;
            case MoveDirTypeEnum.LeftDown:
                nextSpace = new Vector2(transform.position.x - shortcutMoveDistance,
                                        transform.position.y - shortcutMoveDistance);
                break;
        }

        return nextSpace;
    }
}
