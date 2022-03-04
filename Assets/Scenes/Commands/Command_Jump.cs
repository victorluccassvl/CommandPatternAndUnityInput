using UnityEngine;
using LeRatTools;

public class Command_Jump : ICommand
{
    private ForceMode forceMode;
    private float force;

    public Command_Jump(ForceMode mode, float force)
    {
        this.forceMode = mode;
        this.force = force;
    }

    public ForceMode GetForceMode()
    {
        return forceMode;
    }

    public float GetForce()
    {
        return force;
    }
}