using System.Collections;
using System.Collections.Generic;

namespace StateMachine
{
    /// <summary>
    /// интерфейс для управления стейтмашиной
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// метод, переводящий стейтмашину в состояние <paramref name="nextState"/> 
        /// </summary>
        void SwitchState(States nextState);

        /// <summary>
        /// метод, переводящий стейтмашину в предыдущее состояние
        /// </summary>
        void BackToParent();
    }
}

