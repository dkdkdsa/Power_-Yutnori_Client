using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private int tempStepCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move(tempStepCount));
        }
    }

    public IEnumerator Move(int stepCount)
    {
        for (int i = 0; i < stepCount; i++)
        {
            Vector2 nextDir = BoardManager.Instance.GetDirFromPlayerPos(transform.position);

            transform.DOMove(nextDir, 0.2f);
            yield return new WaitForSeconds(0.3f);
        }

        CheckPoint checkPoint = BoardManager.Instance.GetSpace<CheckPoint>(transform.position);

        if (checkPoint != null)
        {
            checkPoint.SetIsCheckPointStart();
        }
    }
}
