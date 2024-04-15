using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Line _curLine;

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
        SetCurLine();
        for (int i = 0; i < stepCount; i++)
        {
            curIdx += 1;

            if (_curLine.Spaces.Length <= curIdx)
            {
                // 현재 라인 벗어나므로 라인을 바꿔 준다.
                SetCurLine();
                curIdx = 0;
            }

            transform.DOMove(_curLine.Spaces[curIdx].transform.position, 0.2f);
            yield return new WaitForSeconds(0.3f);
        }

        //CheckCurSpace();
    }

    private void SetCurLine()
    {
        _curLine = BoardManager.Instance.GetCurLine(transform.position);
    }
}
