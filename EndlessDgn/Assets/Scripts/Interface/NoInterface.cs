using System.Collections.Generic;
using StateMachine;
using UnityEngine;


public class NoInterface : BattleInterfaceState
{
    public NoInterface(States name, GUIController mainController, BattleInterfaceController mouseController) 
        : base(name, mainController, mouseController)
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }
}
