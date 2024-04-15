using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : BaseSpace
{
    [SerializeField]
    private MoveDirTypeEnum _checkPointMoveDirType;
    public MoveDirTypeEnum CheckPointMoveDirType => _checkPointMoveDirType;


    private bool isCheckPointStart;
    public bool IsCheckPointStart => isCheckPointStart;

    public override Vector2 GetShotCutDir()
    {
        Vector2 nextSpace = Vector2.zero;

        if (isCheckPointStart) // ���࿡ üũ ����Ʈ���� ������ ���̶�� ������� ��ȯ
        {
            switch (_checkPointMoveDirType)
            {
                case MoveDirTypeEnum.Left:
                    nextSpace = new Vector2(transform.position.x - 1.5f,
                                            transform.position.y);
                    break;
                case MoveDirTypeEnum.Right:
                    nextSpace = new Vector2(transform.position.x + 1.5f,
                                            transform.position.y);
                    break;
                case MoveDirTypeEnum.Up:
                    nextSpace = new Vector2(transform.position.x,
                                            transform.position.y + 1.5f);
                    break;
                case MoveDirTypeEnum.Down:
                    nextSpace = new Vector2(transform.position.x,
                                            transform.position.y - 1.5f);
                    break;
                case MoveDirTypeEnum.RightDown:
                    nextSpace = new Vector2(transform.position.x + 1.3f,
                                            transform.position.y - 1.3f);
                    break;
                case MoveDirTypeEnum.LeftDown:
                    nextSpace = new Vector2(transform.position.x - 1.3f,
                                            transform.position.y - 1.3f);
                    break;
            }

            isCheckPointStart = false;
            return nextSpace;
        }

        return base.GetDir(); //�ƴϸ� üũ ����Ʈ���� �׳� ���� ��ȯ
    }

    protected override void SetUpSpaceType()
    {
        _spaceType = SpaceTypeEnum.CheckPoint;
    }

    public void SetIsCheckPointStart()
    {
        isCheckPointStart = true;
    }
}
