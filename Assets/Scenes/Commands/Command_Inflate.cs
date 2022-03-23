using LeRatTools;

public class Command_Inflate : ICommand
{
    public void Do(ICommandReceiver receiver, ICommandRecorder recorder = null)
    {
        if (receiver is Resize resize)
        {
            resize.Work();
            if (recorder != null) recorder.AddRecord(this, receiver);
        }
    }
}
