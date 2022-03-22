using LeRatTools;

public class Command_Inflate : ICommand
{
    public void Do(ICommandReceiver receiver)
    {
        if (receiver is Resize resize)
        {
            resize.Work();
        }
    }
}
