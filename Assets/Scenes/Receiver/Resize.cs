using UnityEngine;
using LeRatTools;

public class Resize : ICommandReceiver
{
    // Don't depent on Invoker nor Client
    // Implements Functionality
    // Receive parameters via constructor/setters
    // Gets called inside command

    float scaleModifier;
    Transform transform;
    Vector3 initialValue;

    public Resize(Transform transform)
    {
        this.transform = transform;
        this.initialValue = transform.localScale;
    }

    public void SetParameters(float scaleModifier)
    {
        this.scaleModifier = scaleModifier;
    }

    public void Work()
    {
        transform.localScale *= scaleModifier;
    }

    public void Reset()
    {
        transform.localScale = initialValue;
    }
}
