using System.Collections.Generic;
using StateMachine;
using UnityEngine;

/// <summary>
/// класс состояний интерфейса (от него наследуются все состояния интерфейса)
/// </summary>
public abstract class InterfaceState : State
{
    #region Static fields//(возможно стоит убрать)
    /// <summary>
    /// контейнер для всех объектов интерфейса
    /// </summary>
    protected static List<GameObject> AllUIWindows;

    /// <summary>
    /// ссылка на контроллер интерфейса (возможно стоит создать интерфейс IGUIController)
    /// </summary>
    protected static GUIController _mainController;
    #endregion

    public InterfaceState(State parent, States name, GUIController mainController) : base(parent, name)
    {
        AllUIWindows = new List<GameObject>();
        _mainController = mainController;
    }
}


