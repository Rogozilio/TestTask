namespace DefaultNamespace.AI
{
    public abstract class State
    {
        /// <summary>
        /// Машина состояний определяющая ее состояния
        /// </summary>
        protected StateMachine _movementSM;
        
        protected Computer _computer;
        public State(Computer computer, StateMachine movementSM)
        {
            _movementSM = movementSM;
            _computer = computer;
        }
        /// <summary>
        /// Срабатывает первым. Для определения переменных.
        /// </summary>
        public virtual void Enter()
        {

        }
        /// <summary>
        /// Обработка ввода игрока
        /// </summary>
        public virtual void HandleInput()
        {

        }
        /// <summary>
        /// обработка логики
        /// </summary>
        public virtual void LogicUpdate()
        {

        }
        /// <summary>
        /// обработка физики
        /// </summary>
        public virtual void PhysicsUpdate()
        {

        }
        /// <summary>
        /// Срабатывает последним. Для очистки данных
        /// </summary>
        public virtual void Exit()
        {

        }
    }
}