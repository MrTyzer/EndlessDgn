using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.UI;

public class Idle : BattleInterfaceState
{
    public Idle(States name, GUIController mainController, BattleInterfaceController mouseController)
        : base(name, mainController, mouseController)
    {
    }

    public override void OnEnter()
    {
        Messenger.AddListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnCharacterClick);
    }

    public override void OnExit()
    {
        Messenger.RemoveListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnCharacterClick);
    }

    private void OnCharacterClick()
    {
        _mouseController.ShowStats();
    }
}



