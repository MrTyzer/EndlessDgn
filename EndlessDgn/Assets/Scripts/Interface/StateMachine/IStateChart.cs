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
        /// <param name="exitExt">
        /// при true - вызывает расширение для метода OnExit
        /// </param>
        /// <param name="enterExt">
        /// при true - вызывает расширение для метода OnEnter
        /// </param>
        void SwitchState(States nextState, bool exitExt = true, bool enterExt = true);

        /// <summary>
        /// метод, переводящий стейтмашину в предыдущее состояние
        /// </summary>
        /// <param name="exitExt">
        /// при true - вызывает расширение для метода OnExit
        /// </param>
        /// <param name="enterExt">
        /// при true - вызывает расширение для метода OnEnter
        /// </param>
        void BackToParent(bool exitExt = true, bool enterExt = true);
    }
}

