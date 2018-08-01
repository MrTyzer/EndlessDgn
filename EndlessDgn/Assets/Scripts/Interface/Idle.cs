using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class Idle : InterfaceState
{
    public Idle(State parent, States name, GUIController mainController) : base(parent, name, mainController)
    {
    }

    public override void OnEnter()
    {
        _mainController.AbilityBar.SetActive(true);
        int i = 0;
        foreach (Ability h in GUIController.CurrentHero.SpellBook)
        {
            _mainController.AbilityButtons[i].gameObject.SetActive(true);
            _mainController.AbilityButtons[i].AbilityShow(h);
            i++;
        }
    }

    public override void OnEnterExt()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        _mainController.AbilityBar.SetActive(false);
    }

    public override void OnExitExt()
    {
        throw new System.NotImplementedException();
    }
}



