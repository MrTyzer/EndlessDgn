using System.Collections;
using System.Collections.Generic;

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
            AddParent(parent);
            Name = name;
        }

        /// <summary>
        /// конструктор стейта, у которого нет родителя
        /// </summary>
        public State(States name)
        {
            Name = name;
        }

        /// <summary>
        /// метод назначает родительский стейт и записывает текущий стейт в массив дочерних стейтов у родителя
        /// </summary>
        private void AddParent(State parent)
        {
            ParentState = parent;
            parent.ChildStates.Add(Name, this);
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

