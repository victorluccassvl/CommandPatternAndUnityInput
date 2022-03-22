namespace LeRatTools
{
    public interface ICommandInvoker
    {
        // Depends on Receivers
        // Receives commands from client
        // Chooses when to run those commands

        public void AddCommand<T>(T receivedCommand) where T : ICommand;
    }
}