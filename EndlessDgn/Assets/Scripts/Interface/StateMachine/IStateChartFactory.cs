namespace StateMachine
{
    /// <summary>
    /// интерфейс для добавления и удаления стейтов
    /// </summary>
    public interface IStateChartFactory
    {
        /// <summary>
        /// добавляет стейт в стейтмашину
        /// </summary>
        void AddState(States name, State state);

        /// <summary>
        /// убирает стейт из стейтмашины
        /// </summary>
        void RemoveState(States name, State state);
    }
}