using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Line _curLine;
    private BaseSpace _curSpace;

    [SerializeField]
    private int stepCount;

    private int curIdx;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        for (int i = 0; i < stepCount; i++)
        {
            _curSpace = BoardManager.Instance.GetSpace(transform.position);

            curIdx += 1;

            //if (_curLine.Spaces.Length <= curIdx) // 골인한거임. 아 생각해보니까 이렇게 하면 지름길로 가면 안될듯
            //{
            //    Debug.Log("골인");
            //    Destroy(gameObject);
            //    yield break;
            //}

            Vector2 nextSpace = _curSpace.GetDir();

            transform.DOMove(nextSpace, 0.2f);
            //transform.DOMove(_curLine.Spaces[curIdx].transform.position, 0.2f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void SetCurLine(BaseSpace space)
    {
        bool isCheckPoint = space.SpaceType == SpaceTypeEnum.CheckPoint;

        if (isCheckPoint) // 체크포인트면 지름길로 가야됨
        {
            
        }

        Line nextLine = BoardManager.Instance.GetLine(transform.position);
        if (_curLine != nextLine) // 라인이 다르면 바꿔준다
        {
            _curLine = nextLine;
        }
        
    }
}
