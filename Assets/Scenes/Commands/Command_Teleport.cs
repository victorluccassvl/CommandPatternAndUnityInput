using LeRatTools;
using UnityEngine;

public class Command_Teleport : ICommand
{
    private Vector3 teleport;

    public Command_Teleport(float teleportDistance)
    {
        this.teleport = Vector3.up * teleportDistance;
    }

    public Vector3 GetTeleport()
    {
        return teleport;
    }
}