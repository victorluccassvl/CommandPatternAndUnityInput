using UnityEngine;
using LeRatTools;

public class Rotate : ICommandReceiver
{
    // Don't depent on Invoker nor Client
    // Implements Functionality
    // Receive parameters via constructor/setters
    // Gets called inside command

    float force;
    Rigidbody rigidbody;
    Vector3 eulerRotation;

    public Rotate(Rigidbody rigidbody, float force)
    {
        this.rigidbody = rigidbody;
        this.force = force;
    }
    
    public void SetParameters(Vector3 eulerRotation)
    {
        this.eulerRotation = eulerRotation;
    }

    public void Work()
    {
        rigidbody.AddTorque(eulerRotation * force, ForceMode.Impulse);
    }
}