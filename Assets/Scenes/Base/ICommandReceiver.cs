namespace LeRatTools
{
    public interface ICommandReceiver
    {
        // Don't depent on Invoker nor Client
        // Implements Functionality
        // Receive parameters via constructor/setters
        // Gets called inside command

        public void Work();
    }
}
