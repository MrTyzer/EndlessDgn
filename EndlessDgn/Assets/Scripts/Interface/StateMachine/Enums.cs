namespace StateMachine
{
    /// <summary>
    /// перечисление всех возможных состояний
    /// </summary>
    public enum States
    {
        /// <summary>
        /// состояние, при котором весь интерфейс отключен
        /// </summary>
        NoInterface,
        /// <summary>
        /// базовое состояние интерфейса
        /// </summary>
        Idle,
        /// <summary>
        /// состояние со всплывающим окном статов персонажей
        /// </summary>
        TakeStats,
        /// <summary>
        /// состояние выбора таргета при использовании способности
        /// </summary>
        SelectTarget
    }

}
