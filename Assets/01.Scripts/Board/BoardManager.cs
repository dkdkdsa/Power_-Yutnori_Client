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

    private Dictionary<Vector3, BaseSpace> _cachedSpaces // 캐싱용 딕셔너리
        = new Dictionary<Vector3, BaseSpace>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;
    }

    public T GetSpace<T>(Vector3 playerPos) where T : BaseSpace // 플레이어가 현재 있는 칸 
    {
        // 이미 캐싱된 결과가 있는지 확인
        if (_cachedSpaces.ContainsKey(playerPos))
        {
            // 이미 캐싱된 결과가 있으면 그것을 반환
            return _cachedSpaces[playerPos] as T;
        }

        Collider2D collider = Physics2D.Raycast(playerPos, Vector3.forward, 10f).collider;

        if (collider.TryGetComponent(out T component) &&
            collider != null)
        {
            // 결과를 캐싱하고 반환
            _cachedSpaces.Add(playerPos, component);
            return component;
        }
        return null;
    }

    public Vector2 GetDirFromPlayerPos(Vector3 playerPos) // 현재 있는 칸에 따른 이동 방향
    {
        bool isEndSpace = GetSpace<EndPoint>(playerPos) != null;

        if (isEndSpace) // 골인
        {
            return Vector2.zero;
        }

        CheckPoint curSpace = GetSpace<CheckPoint>(playerPos);

        if (curSpace != null) // 체크 포인트면 체크포인트의 지름길 반환하려고 시도
        {
            return curSpace.GetShotCutDir();
        }

        return GetSpace<BaseSpace>(playerPos).GetDir(); // 아니라면 그냥 바로 방향 반환
    }
}
