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
        /// <param name="noExit">
        /// При true - не выходит из предыдущего стейта
        /// </param>
        void SwitchState(States nextState, bool noExit = false);

        /// <summary>
        /// метод, переводящий стейтмашину в предыдущее состояние
        /// </summary>
        void BackToParent();
    }
}

