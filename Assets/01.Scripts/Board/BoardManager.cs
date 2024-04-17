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

    private Dictionary<Vector3, BaseSpace> _cachedSpaces // ĳ�̿� ��ųʸ�
        = new Dictionary<Vector3, BaseSpace>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;
    }

    public T GetSpace<T>(Vector3 playerPos) where T : BaseSpace // �÷��̾ ���� �ִ� ĭ 
    {
        // �̹� ĳ�̵� ����� �ִ��� Ȯ��
        if (_cachedSpaces.ContainsKey(playerPos))
        {
            // �̹� ĳ�̵� ����� ������ �װ��� ��ȯ
            return _cachedSpaces[playerPos] as T;
        }

        Collider2D collider = Physics2D.Raycast(playerPos, Vector3.forward, 10f).collider;

        if (collider.TryGetComponent(out T component) &&
            collider != null)
        {
            // ����� ĳ���ϰ� ��ȯ
            _cachedSpaces.Add(playerPos, component);
            return component;
        }
        return null;
    }

    public Vector2 GetDirFromPlayerPos(Vector3 playerPos) // ���� �ִ� ĭ�� ���� �̵� ����
    {
        bool isEndSpace = GetSpace<EndPoint>(playerPos) != null;

        if (isEndSpace) // ����
        {
            return Vector2.zero;
        }

        CheckPoint curSpace = GetSpace<CheckPoint>(playerPos);

        if (curSpace != null) // üũ ����Ʈ�� üũ����Ʈ�� ������ ��ȯ�Ϸ��� �õ�
        {
            return curSpace.GetShotCutDir();
        }

        return GetSpace<BaseSpace>(playerPos).GetDir(); // �ƴ϶�� �׳� �ٷ� ���� ��ȯ
    }
}
