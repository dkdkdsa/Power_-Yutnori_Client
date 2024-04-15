using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseSpace : MonoBehaviour
{
    protected SpaceTypeEnum _spaceType;
    public SpaceTypeEnum SpaceType => _spaceType;

    [SerializeField]
    protected MoveDirTypeEnum _moveDirType;
    public MoveDirTypeEnum MoveDirType => _moveDirType;

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

    public Vector2 GetDir()
    {
        Vector2 nextSpace = Vector2.zero;
        switch (_moveDirType)
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
                nextSpace = new Vector2(transform.position.x + 1.4f,
                                        transform.position.y - 1.4f);
                break;
            case MoveDirTypeEnum.LeftDown:
                nextSpace = new Vector2(transform.position.x - 1.4f,
                                        transform.position.y - 1.4f);
                break;
        }
        return nextSpace;
    }
}
