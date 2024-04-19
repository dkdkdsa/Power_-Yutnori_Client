using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int tempStepCount;

    private bool isPiecedOnBoard = false;
    private bool isArrived = false;

    [SerializeField]
    private float moveSpeed = 0.2f;
    [SerializeField]
    private float movedelay = 0.3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move(tempStepCount));
        }
    }

    public IEnumerator Move(int stepCount)
    {
        if(!isPiecedOnBoard)
        {
            isPiecedOnBoard = true;
            Vector2 nextDir = new Vector2(transform.position.x,
                                        transform.position.y + 1.5f);
            transform.DOMove(nextDir, moveSpeed);
            stepCount--;
            yield return new WaitForSeconds(movedelay);
        }

        for (int i = 0; i < stepCount; i++)
        {
            Vector2 nextDir = BoardManager.Instance.GetDirFromPlayerPos(transform.position);

            if(nextDir == Vector2.zero) // °ñÀÎÇÑ°Ü
            {
                Debug.Log("°ñ");
                isArrived = true;
                Destroy(gameObject); // ÀÏ´ÜÀº °Á Áö¿ï°Ô
            }

            transform.DOMove(nextDir, moveSpeed);
            yield return new WaitForSeconds(movedelay);
        }

        CheckPoint checkPoint = BoardManager.Instance.GetSpace<CheckPoint>(transform.position);

        if (checkPoint != null)
        {
            checkPoint.SetIsCheckPointStart();
        }
    }
}
