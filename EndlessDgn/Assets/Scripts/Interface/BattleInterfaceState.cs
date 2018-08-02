using System.Collections.Generic;
using StateMachine;
using UnityEngine;

/// <summary>
/// класс состояний интерфейса (от него наследуются все состояния интерфейса)
/// </summary>
public abstract class BattleInterfaceState : State
{
    #region Static fields//(возможно стоит убрать)
    /// <summary>
    /// ссылка на контроллер интерфейса (возможно стоит создать интерфейс IGUIController)
    /// </summary>
    protected GUIController _mainController;

    protected BattleInterfaceController _mouseController; 
    #endregion

    public BattleInterfaceState(States name, GUIController mainController, BattleInterfaceController mouseController) : base(name)
    {
        _mainController = mainController;
        _mouseController = mouseController;
    }
}


