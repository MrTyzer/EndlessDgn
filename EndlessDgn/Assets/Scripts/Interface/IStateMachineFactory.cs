using System.Collections;
using System.Collections.Generic;

namespace StateMachine
{
    /// <summary>
    /// интерфейс для добавления и удаления стейтов
    /// </summary>
    public interface IStateMachineFactory
    {
        /// <summary>
        /// добавляет стейт в стейтмашину
        /// </summary>
        void AddState(States name, State state);

        /// <summary>
        /// убирает стейт из стейтмашины
        /// </summary>
        void RemoveState(States name, State state);
    }
}