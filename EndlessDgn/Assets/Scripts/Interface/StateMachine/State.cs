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
        public Dictionary<States, State> ChildStates { get; private set; }
        public State ParentState { get; private set; }

        /// <summary>
        /// конструктор для стейта, у которого есть родитель
        /// </summary>
        public State(State parent, States name)
        {
            Name = name;
            ChildStates = new Dictionary<States, State>();
            if (parent != null)
            {
                AddParent(parent);
            }
        }

        /// <summary>
        /// метод назначает родительский стейт и записывает текущий стейт в массив дочерних стейтов у родителя
        /// </summary>
        private void AddParent(State parent)
        {
            ParentState = parent;
            parent.ChildStates.Add(this.Name, this);
        }

        /// <summary>
        /// метод входа в стейт
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// расширение (необязательное) для метода входа в стейт (выполняется после OnEnter)
        /// </summary>
        public abstract void OnEnterExt();

        /// <summary>
        /// метод выхода из стейта
        /// </summary>
        public abstract void OnExit();
        
        /// <summary>
        /// расширение (необязательное) для метода выхода из стейта (выполняется после OnExit)
        /// </summary>
        public abstract void OnExitExt();
    }
}

