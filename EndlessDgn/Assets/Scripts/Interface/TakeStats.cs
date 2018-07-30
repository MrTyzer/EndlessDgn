using System.Collections.Generic;
using StateMachine;
using UnityEngine;


public class TakeStats : InterfaceState
{
    public TakeStats(State parent, States name, GUIController mainController) : base(parent, name, mainController)
    {
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}


