using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityNet;

public class SpawnPlayerButton : MonoBehaviour
{
    private PlayerCountUI _playerCountUI;

    private PlayersController _playersController;

    private Button _button;

    private void OnEnable()
    {
        SignalHub.OnSetEnableButtonEvent += EnableButton;
        TurnManager.Instance.OnChangeTurnEvent += DisableButton;
    }

    private void OnDisable()
    {
        SignalHub.OnSetEnableButtonEvent -= EnableButton;
        TurnManager.Instance.OnChangeTurnEvent -= DisableButton;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _playersController = FindObjectOfType<PlayersController>();
        _playerCountUI = transform.parent.GetComponent<PlayerCountUI>();
    }

    private void Start()
    {
        DisableButton();
    }

    private void EnableButton()
    {
        _button.interactable = _playerCountUI.ComparePlayerType((int)TurnManager.Instance.CurTurnType);
    }

    private void DisableButton()
    {
        _button.interactable = false;
    }

    public void SpawnButtonClickHandler()
    {
        _playersController.SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);

        //if (ScoreAndSpawnManager.Instance.IsSpawnButtonClick && ScoreAndSpawnManager.Instance.SpawnCount > 0)
        //{
        //    _playersController.SpawnPlayer((PlayerType)NetworkManager.Instance.ClientId - 1);

        //}
    }

}
