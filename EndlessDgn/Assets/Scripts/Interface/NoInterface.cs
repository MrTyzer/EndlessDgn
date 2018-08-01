using System.Collections.Generic;
using StateMachine;
using UnityEngine;


public class NoInterface : InterfaceState
{
    public NoInterface(State parent, States name, GUIController mainController) : base(parent, name, mainController)
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnEnterExt()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
    }

    public override void OnExitExt()
    {
        throw new System.NotImplementedException();
    }
}
