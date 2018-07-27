using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// машина состояний для создания меню(главного и возможно внутриигрового)
    /// </summary>
    public sealed class MenuStateMachine : IStateMachine, IStateMachineFactory
    {
        /// <summary>
        /// массив состояний, соответствующий всем вкладкам меню
        /// </summary>
        private Dictionary<States, State> _states;

        /// <summary>
        /// начальное состояние стейтмашины(вкладка меню)
        /// </summary>
        private State _currentState;

        public MenuStateMachine(Dictionary<States, State> states, State currentState)
        {
            _currentState = currentState;
            _states = states;
            _currentState.OnEnter();
        }

        #region IStateMashine methods

        public void AddState(States name, State state)
        {
            _states[name] = state;
        }

        public void RemoveState(States name, State state)
        {
            _states[name].ParentState.ChildStates.Remove(name);//убираем стейт из коллекции дочерних стейтов у стейта-родителя
            _states.Remove(name);
        }

        public void SwitchState(States nextState)
        {
            if (_currentState.ChildStates.ContainsKey(nextState))
            {
                _currentState.OnExit();
                _currentState.ChildStates[nextState].OnEnter();
                _currentState = _states[nextState];
            }
            else
            {
                //TODO: прописать ошибку
            }
        }


        public void BackToParent()
        {
            if (_currentState.ParentState != null)
            {
                _currentState.OnExit();
                _currentState.ParentState.OnEnter();
                _currentState = _currentState.ParentState;
            }
            else
            {
                //TODO: прописать ошибку
            }
        }
        #endregion
    }
}