using UnityEngine;
using LeRatTools;

public class Command_Rotate : ICommand
{
    private Vector3 eulerAngles;

    public Command_Rotate(Vector3 eulerAngles)
    {
        this.eulerAngles = eulerAngles;
    }

    public void Do(ICommandReceiver receiver)
    {
        if (receiver is Rotate rotate)
        {
            rotate.SetParameters(eulerAngles);
            rotate.Work();
        }
    }
}