using System.Collections.Generic;
using StateMachine;
using UnityEngine;


public class SelectTarget : BattleInterfaceState
{
    public SelectTarget(States name, GUIController mainController, BattleInterfaceController mouseController) 
        : base(name, mainController, mouseController)
    {
    }

    public override void OnEnter()
    {
        Messenger.AddListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnSelectTarget);
    }

    /// <summary>
    /// метод выбора таргета для способности
    /// </summary>
    private void OnSelectTarget()
    {
       _mouseController.GetTargetForAbility();
    }

    public override void OnExit()
    {
        Messenger.RemoveListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnSelectTarget);
    }

}
