namespace Utility.StateSystem
{
    public interface IStateMachine
    {
        public void SetState<T>() where T : State;
    }
}