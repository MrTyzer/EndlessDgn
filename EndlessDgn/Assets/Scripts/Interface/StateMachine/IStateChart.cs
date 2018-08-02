namespace StateMachine
{
    /// <summary>
    /// интерфейс для управления стейтмашиной
    /// </summary>
    public interface IStateChart
    {
        /// <summary>
        /// метод, переводящий стейтмашину в состояние <paramref name="nextState"/> 
        /// </summary>
        void SwitchState(States nextState);
    }
}

