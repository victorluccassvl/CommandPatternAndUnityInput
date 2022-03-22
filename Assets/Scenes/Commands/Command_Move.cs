using UnityEngine;
using LeRatTools;

public class Command_Move : ICommand
{
    private Vector3 direction;

    public Command_Move(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Do(ICommandReceiver receiver)
    {
        if (receiver is Move move)
        {
            move.SetParameters(direction);
            move.Work();
        }
    }
}