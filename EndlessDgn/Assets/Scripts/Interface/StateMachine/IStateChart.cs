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
        /// <param name="noExit">
        /// При true - не вызывает OnExit предыдущего стейта
        /// </param>
        void SwitchState(States nextState, bool noExit = false);

        /// <summary>
        /// метод, переводящий стейтмашину в предыдущее состояние
        /// </summary>
        /// <param name="noEnter">
        /// При true - не вызывает OnEnter родителя
        /// </param>
        void BackToParent(bool noEnter = false);
    }
}

