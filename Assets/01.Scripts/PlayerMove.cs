using DG.Tweening;
using System;
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

            if (_curLine.Spaces.Length <= curIdx) // 골인한거임
            {
                Debug.Log("골인");
                Destroy(gameObject);
                yield break;
            }

            transform.DOMove(_curLine.Spaces[curIdx].transform.position, 0.2f);
            yield return new WaitForSeconds(0.3f);
        }

        CheckCurSpace();
    }

    private void CheckCurSpace()
    {
        BaseSpace space = BoardManager.Instance.GetSpace(transform.position);

        //여기서 지름길 해줄듯
    }

    private void SetCurLine()
    {
        _curLine = BoardManager.Instance.GetLine(transform.position);
    }
}
