using LeRatTools;

public class Command_Jump : ICommand
{
    public void Do(ICommandReceiver receiver)
    {
        if (receiver is Jump jump)
        {
            jump.Work();
        }
    }
}