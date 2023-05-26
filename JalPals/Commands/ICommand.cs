namespace JalPals.Commands
{
    public interface ICommand
    {
        ExecutionStatus Status { get; set; }
        void Execute();
    }
}

