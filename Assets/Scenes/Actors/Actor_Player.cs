using LeRatTools;
using UnityEngine;

public class Actor_Player : Actor<Actor_Player>
{
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    protected override void ExecuteCommand(ICommand command)
    {
        switch (command)
        {
            case Command_Deflate  c: ExecuteCommand(c); break;
            case Command_Inflate  c: ExecuteCommand(c); break;
            case Command_Move     c: ExecuteCommand(c); break;
            case Command_Rotate   c: ExecuteCommand(c); break;
            case Command_Teleport c: ExecuteCommand(c); break;
            case Command_Jump     c: ExecuteCommand(c); break;
        }
    }

    public void ExecuteComand(Command_Deflate command)
    {
        transform.localScale *= command.GetRatio();
    }
    public void ExecuteComand(Command_Jump command)
    {
        playerRigidbody.AddForce(Vector3.up * command.GetForce(), command.GetForceMode());
    }
    public void ExecuteComand(Command_Move command)
    {
        playerRigidbody.MovePosition(transform.position + command.GetMovement());
    }
    public void ExecuteComand(Command_Inflate command)
    {
        transform.localScale *= command.GetRatio();
    }
    public void ExecuteComand(Command_Rotate command)
    {
        playerRigidbody.AddTorque(command.GetEulerRotation());
    }
    public void ExecuteComand(Command_Teleport command)
    {
        playerRigidbody.MovePosition(transform.position + command.GetTeleport());
    }

    protected override void AddHistory(ICommand command)
    {
        throw new System.NotImplementedException();
    }

    protected override void ClearHistory()
    {
        throw new System.NotImplementedException();
    }

    protected override ICommand GetLastCommand()
    {
        throw new System.NotImplementedException();
    }
}
