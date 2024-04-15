using DG.Tweening;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;
    }

    public T GetSpace<T>(Vector3 playerPos) where T : BaseSpace// 이것들 나중에 다 캐싱
    {
        Collider2D collider = Physics2D.Raycast(playerPos, Vector3.forward, 10f)
            .collider;

        if (collider.TryGetComponent(out T component))
        {
            return component;
        }
        return null;
    }

    public Vector2 GetDirFromPlayerPos(Vector3 playerPos)
    {
        CheckPoint curSpace = GetSpace<CheckPoint>(playerPos);

        if (curSpace != null) // 체크 포인트면 체크포인트의 지름길 반환하려고 시도
        {
            return curSpace.GetShotCutDir();
        }

        return GetSpace<BaseSpace>(playerPos).GetDir(); // 아니라면 그냥 바로 방향 반환
    }
}
