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

    public T GetSpace<T>(Vector3 playerPos) where T : BaseSpace// �̰͵� ���߿� �� ĳ��
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

        if (curSpace != null) // üũ ����Ʈ�� üũ����Ʈ�� ������ ��ȯ�Ϸ��� �õ�
        {
            return curSpace.GetShotCutDir();
        }

        return GetSpace<BaseSpace>(playerPos).GetDir(); // �ƴ϶�� �׳� �ٷ� ���� ��ȯ
    }
}
