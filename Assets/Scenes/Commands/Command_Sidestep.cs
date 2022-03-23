using LeRatTools;

public class Command_Sidestep : ICommand
{
    public void Do(ICommandReceiver receiver, ICommandRecorder recorder = null)
    {
        if (receiver is Sidestep sidestep)
        {
            sidestep.Work();
            if (recorder != null) recorder.AddRecord(this, receiver);
        }
    }
}