using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    private Line[] _lines;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("BoardManager is multiple");
        }

        Instance = this;
    }

    private void SetLines()
    {
        _lines = GetComponentsInChildren<Line>();
    }

    public Line GetCurLine(Vector3 playerPos)
    {
        RaycastHit2D space = Physics2D.Raycast(playerPos, Vector3.forward, 10f);
        return space.transform.parent.GetComponent<Line>();
    }

    //public Line GetNextLine(Vector3 playerPos)
    //{
    //    RaycastHit2D space = Physics2D.Raycast(playerPos, Vector3.forward, 10f);
        
    //}
}
