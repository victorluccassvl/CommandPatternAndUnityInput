using UnityEngine;
using LeRatTools;

public class Command_Rotate : ICommand
{
    private Vector3 eulerRotation;

    public Command_Rotate(Vector2 input, float rotation)
    {
        input.Normalize();
        input *= rotation;
        eulerRotation = Vector3.zero;
        eulerRotation.x = input.x;
        eulerRotation.z = input.y;
    }

    public Vector3 GetEulerRotation()
    {
        return -eulerRotation;
    }
}