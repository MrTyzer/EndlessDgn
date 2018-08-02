using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// базовый класс для всех стейтов
    /// </summary>
    public abstract class State
    {
        public States Name { get; private set; }

        /// <summary>
        /// конструктор для стейта, у которого есть родитель
        /// </summary>
        public State(States name)
        {
            Name = name;
        }

        /// <summary>
        /// метод входа в стейт
        /// </summary>
        public abstract void OnEnter();
        
        /// <summary>
        /// метод выхода из стейта
        /// </summary>
        public abstract void OnExit();
    }
}

