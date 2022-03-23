using UnityEngine;
using LeRatTools;

public class Sidestep : ICommandReceiver
{
    // Don't depent on Invoker nor Client
    // Implements Functionality
    // Receive parameters via constructor/setters
    // Gets called inside command

    float force;
    Rigidbody rigidbody;
    bool right;

    public Sidestep(Rigidbody rigidbody, float force)
    {
        this.rigidbody = rigidbody;
        this.force = force;
        this.right = true;
    }

    public void SetParameters(float force)
    {
        this.force = force;
    }

    public void Work()
    {
        float direction = (right) ? 1f : -1f;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(direction * Vector3.right * force, ForceMode.Impulse);
        right = !right;
    }
}
