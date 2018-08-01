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
    public static IStateChart GetInterfaceSC(GUIController controller, Camera camera)
    {
        State noInterface = new NoInterface(null, States.NoInterface, controller);
        State idle = new Idle(noInterface, States.Idle, controller, camera);
        State selectTarget = new SelectTarget(idle, States.SelectTarget, controller, camera);
        State takeStats = new TakeStats(idle, States.TakeStats, controller);

        List<State> states = new List<State>()
        {
            noInterface,
            idle,
            selectTarget,
            takeStats
        };

        StateChart sc = new StateChart(states, noInterface);

        return sc;
    }
}

