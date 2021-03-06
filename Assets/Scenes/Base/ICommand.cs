namespace LeRatTools
{
    public interface ICommand
    {
        public virtual void Do(ICommandReceiver receiver, ICommandRecorder recorder = null)
        {
            receiver.Work();
        }
    }
}