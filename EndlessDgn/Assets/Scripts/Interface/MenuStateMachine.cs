using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

/// <summary>
/// машина состояний для создания меню(главного и возможно внутриигрового)
/// </summary>
public class MenuStateMachine 
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

    public void AddState(States name, State state)
    {
        _states[name] = state;
    }

    /// <summary>
    /// метод, переводящий стейтмашину в состояние <paramref name="nextState"/> 
    /// </summary>
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

    /// <summary>
    /// метод, переводящий стейтмашину в предыдущее состояние
    /// </summary>
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

    
}
