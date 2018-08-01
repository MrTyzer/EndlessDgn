using System.Collections;
using System.Collections.Generic;

namespace StateMachine
{
    /// <summary>
    /// машина состояний
    /// </summary>
    public class StateChart : IStateChart, IStateChartFactory
    {
        /// <summary>
        /// массив состояний, соответствующий всем вкладкам меню
        /// </summary>
        private Dictionary<States, State> _states = new Dictionary<States, State>();

        /// <summary>
        /// начальное состояние стейтмашины(вкладка меню)
        /// </summary>
        private State _currentState;

        public StateChart(List<State> states, State currentState)
        {
            _currentState = currentState;
            foreach (State s in states)
            {
                _states[s.Name] = s;
            }
            _currentState.OnEnter();
        }

        #region IStateMashineFactory methods

        public void AddState(States name, State state)
        {
            _states[name] = state;
        }

        public void RemoveState(States name, State state)
        {
            _states[name].ParentState.ChildStates.Remove(name);//убираем стейт из коллекции дочерних стейтов у стейта-родителя
            _states.Remove(name);
        }

        #endregion

        #region IStateMashine methods

        public void SwitchState(States nextState, bool exitExt = false, bool enterExt = false)
        {
            if (_currentState.ChildStates.ContainsKey(nextState))
            {
                _currentState.OnExit();
                if (exitExt)
                    _currentState.OnExitExt();

                _currentState.ChildStates[nextState].OnEnter();
                if (enterExt)
                    _currentState.ChildStates[nextState].OnEnterExt();

                _currentState = _states[nextState];
            }
            else
            {
                //TODO: прописать ошибку
            }
        }

        public void BackToParent(bool exitExt = false, bool enterExt = false)
        {
            if (_currentState.ParentState != null)
            {
                _currentState.OnExit();
                if (exitExt)
                    _currentState.OnExitExt();

                _currentState.ParentState.OnEnter();
                if (enterExt)
                    _currentState.ParentState.OnEnterExt();

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