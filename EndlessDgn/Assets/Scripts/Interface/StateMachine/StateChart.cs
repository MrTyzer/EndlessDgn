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
            _states.Remove(name);
        }

        #endregion

        #region IStateMashine methods

        public void SwitchState(States nextState)
        {
            if (_states.ContainsKey(nextState))
            {
                _currentState.OnExit();
                _states[nextState].OnEnter();
                _currentState = _states[nextState];
            }
            else
            {
                //TODO: прописать ошибку
            }
        }
        #endregion
    }
}