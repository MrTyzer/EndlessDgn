using System.Collections.Generic;
using StateMachine;
using UnityEngine;

/// <summary>
/// класс для создания диаграмм состояний
/// </summary>
public static class StateChartFactory
{
    /// <summary>
    /// метод для создания диаграммы состояний интерфейса
    /// </summary>
    public static IStateChart GetInterfaceSC(GUIController controller, BattleInterfaceController mouseController)
    {
        State noInterface = new NoInterface(States.NoInterface, controller, mouseController);
        State idle = new Idle(States.Idle, controller, mouseController);
        State selectTarget = new SelectTarget(States.SelectTarget, controller, mouseController);

        List<State> states = new List<State>()
        {
            noInterface,
            idle,
            selectTarget
        };

        StateChart sc = new StateChart(states, noInterface);
        return sc;
    }
}

