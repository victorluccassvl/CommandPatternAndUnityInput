using LeRatTools;

public class Command_Jump : ICommand
{
    public void Do(ICommandReceiver receiver, ICommandRecorder recorder = null)
    {
        if (receiver is Jump jump)
        {
            jump.Work();
            if (recorder != null) recorder.AddRecord(this, receiver);
        }
    }
}