namespace DefaultNamespace.AI
{
    public class StateShot : State
    {
        public StateShot(Computer computer, StateMachine movementSM) : base(computer, movementSM)
        {
            _movementSM = movementSM;
            _computer = computer;
        }
        
        public override void LogicUpdate()
        {
            _computer.TakeShot();
            _movementSM.ChangeState(_computer.StateMove);
        }
    }
}