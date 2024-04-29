using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCountUI : MonoBehaviour
{
    [SerializeField]
    private PlayerType _platyerTurnType;
    public PlayerType PlatyerTurnType => _platyerTurnType;

    private Stack<Image> _images = new Stack<Image>();

    private Stack<Image> _removeUIs = new Stack<Image>();


    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>();

        for(int i = 0; i < images.Length; i++)
        {
            _images.Push(images[i]);
        }
    }

    private void Start()
    {
        ScoreAndSpawnManager.Instance.OnSpawnPlayer += RemovePlayerUI;
        ScoreAndSpawnManager.Instance.OnPlayerCatch += AddPlayerUI;
    }

    public void RemovePlayerUI(PlayerType playerType)
    {
        if (ComparePlayerType((int)playerType))
        {
            Image removeUI = _images.Pop();
            removeUI.enabled = false;
            _removeUIs.Push(removeUI);
        }
    }

    public void AddPlayerUI(PlayerType playerType, int count)
    {
        if (ComparePlayerType((int)playerType))
        {
            for (int i = 0; i < count; i++)
            {

                Image addUI = _removeUIs.Pop();
                addUI.enabled = true;
                _images.Push(addUI);
            }
        }
    }

    public bool ComparePlayerType(int playerType) => playerType == (int)_platyerTurnType;
}
