using System;
using System.Collections.Generic;
namespace JalPals.Commands
{
    public class CommandManager
    {
        private Game1 game;
        private Queue<ICommand> commandQueue;
        private Stack<ICommand> commandHistory;

        public CommandManager(Game1 game)
        {
            this.game = game;
            commandQueue = new Queue<ICommand>();
            commandHistory = new Stack<ICommand>();
        }

        public void Update()
        {
            DrainCommandQueue(commandQueue);
        }

        public void AddNewCommand(ICommand command)
        {
            command.Status = ExecutionStatus.Pending;
            commandQueue.Enqueue(command);
        }

        private void DrainCommandQueue(Queue<ICommand> queue)
        {
            while (queue.Count > 0)
            {
                ICommand command = queue.Dequeue();
                commandHistory.Push(command);
                try
                {
                    command.Execute();
                    command.Status = ExecutionStatus.Successful;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Command execution failed.\n" + e);
                    command.Status = ExecutionStatus.Failed;
                }
            }
        }
    }
}

