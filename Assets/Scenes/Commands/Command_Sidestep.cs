using LeRatTools;

public class Command_Sidestep : ICommand
{
    public void Do(ICommandReceiver receiver)
    {
        if (receiver is Sidestep sidestep)
        {
            sidestep.Work();
        }
    }
}