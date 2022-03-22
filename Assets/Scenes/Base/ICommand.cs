namespace LeRatTools
{
    public interface ICommand
    {
        public virtual void Do(ICommandReceiver receiver)
        {
            receiver.Work();
        }
    }
}