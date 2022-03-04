using UnityEngine;
using LeRatTools;

public class Command_Move : ICommand
{
    private Vector3 movement;

    public Command_Move(Vector2 input, float moveDistance)
    {
        input.Normalize();
        input *= moveDistance;
        movement = Vector3.zero;
        movement.x = input.y;
        movement.z = -input.x;
    }

    public Vector3 GetMovement()
    {
        return movement;
    }
}