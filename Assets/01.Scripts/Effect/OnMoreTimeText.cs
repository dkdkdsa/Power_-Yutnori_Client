using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityNet;

public class OnMoreTimeText : NetBehavior
{

    private TMP_Text text;

    private void Awake()
    {
        
        text = GetComponent<TMP_Text>();
        var target = text.color;
        target.a = 0;
        text.DOFade(0, 1.5f).OnComplete(() =>
        {

            if (NetObject.IsOwner)
            {

                NetObject.Despawn();

            }

        });
        

    }

}
