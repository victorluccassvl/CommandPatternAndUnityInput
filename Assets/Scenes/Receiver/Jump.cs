using UnityEngine;
using LeRatTools;

public class Jump : ICommandReceiver
{
    // Don't depent on Invoker nor Client
    // Implements Functionality
    // Receive parameters via constructor/setters
    // Gets called inside command

    Rigidbody rigidbody;
    float force;

    public Jump(Rigidbody rigidbody, float force)
    {
        this.rigidbody = rigidbody;
        this.force = force;
    }

    public void SetParameters(float force)
    {
        this.force = force;
    }

    public void Work()
    {
        rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}