using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut : MonoBehaviour
{

    private Vector3 startPos;

    private void Awake()
    {

        startPos = transform.localPosition;

    }

    public Sequence JumpAndRotate()
    {

        Sequence seq = DOTween.Sequence();

        transform.localPosition = startPos;

        seq.Append(transform.DOLocalJump(transform.localPosition + (Vector3)Random.insideUnitCircle * 25, 500, 1, 1f).SetEase(Ease.OutQuad));
        seq.Join(transform.DOLocalRotate(new Vector3(0, 0, Random.Range(720f * 3, 720f * 5)), 1f, RotateMode.FastBeyond360));

        return seq;

    }

}
