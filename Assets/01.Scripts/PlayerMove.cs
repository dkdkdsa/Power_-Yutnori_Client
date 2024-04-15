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

            //if (_curLine.Spaces.Length <= curIdx) // �����Ѱ���. �� �����غ��ϱ� �̷��� �ϸ� ������� ���� �ȵɵ�
            //{
            //    Debug.Log("����");
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

        if (isCheckPoint) // üũ����Ʈ�� ������� ���ߵ�
        {
            
        }

        Line nextLine = BoardManager.Instance.GetLine(transform.position);
        if (_curLine != nextLine) // ������ �ٸ��� �ٲ��ش�
        {
            _curLine = nextLine;
        }
        
    }
}
