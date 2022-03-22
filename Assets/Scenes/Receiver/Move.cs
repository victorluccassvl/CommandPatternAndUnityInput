using UnityEngine;
using LeRatTools;

public class Move : ICommandReceiver
{
    // Don't depent on Invoker nor Client
    // Implements Functionality
    // Receive parameters via constructor/setters
    // Gets called inside command

    float speedModifier;
    Rigidbody rigidbody;
    Vector3 movement;

    public Move(Rigidbody rigidbody, float speedModifier)
    {
        this.rigidbody = rigidbody;
        this.speedModifier = speedModifier;
    }

    public void SetParameters(Vector3 movement)
    {
        this.movement = movement;
    }

    public void Work()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speedModifier);
    }
}