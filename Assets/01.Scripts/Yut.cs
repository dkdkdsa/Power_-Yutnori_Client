using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut : MonoBehaviour
{
    
    public Sequence JumpAndRotate()
    {

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalJump(transform.localPosition + (Vector3)Random.insideUnitCircle * 25, 500, 1, 1f).SetEase(Ease.OutQuad));
        seq.Join(transform.DOLocalRotate(new Vector3(0, 0, Random.Range(720f * 3, 720f * 5)), 1f, RotateMode.FastBeyond360));

        return seq;

    }

}
