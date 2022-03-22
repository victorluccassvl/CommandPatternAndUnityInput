using System.Collections.Generic;
using UnityEngine;
using LeRatTools;

public class Player : MonoBehaviour, ICommandInvoker
{
    // Depends on Receivers
    // Receives commands from clients
    // Chooses when to run those commands

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sidestepForce;
    [SerializeField] private float inflateRatio;
    [SerializeField] private float deflateRatio;

    private Rigidbody playerRigidbody;

    private Jump jump;
    private Move move;
    private Resize resize;
    private Rotate rotate;
    private Sidestep sidestep;

    private Queue<ICommand> deflateCommand = new Queue<ICommand>();
    private Queue<ICommand> inflateCommand = new Queue<ICommand>();
    private Queue<ICommand> moveCommand = new Queue<ICommand>();
    private Queue<ICommand> rotateCommand = new Queue<ICommand>();
    private ICommand jumpCommand;
    private ICommand sidestepCommand;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // Initialize command receivers
        jump = new Jump(playerRigidbody, jumpForce);
        move = new Move(playerRigidbody, moveSpeed);
        resize = new Resize(transform);
        rotate = new Rotate(playerRigidbody, rotationForce);
        sidestep = new Sidestep(playerRigidbody, sidestepForce);
    }

    private void FixedUpdate()
    {
        ExecuteCommands();
    }

    private void ExecuteCommands()
    {
        if (jumpCommand != null)
        {
            jumpCommand.Do(jump);
            jumpCommand = null;
        }

        if (sidestepCommand != null)
        {
            sidestepCommand.Do(sidestep);
            sidestepCommand = null;
        }

        while (rotateCommand.Count != 0)
        {
            rotateCommand.Dequeue().Do(rotate);
        }

        while (moveCommand.Count != 0)
        {
            moveCommand.Dequeue().Do(move);
        }

        while (inflateCommand.Count != 0)
        {
            resize.SetParameters(inflateRatio);
            inflateCommand.Dequeue().Do(resize);
        }

        while (deflateCommand.Count != 0)
        {
            resize.SetParameters(deflateRatio);
            deflateCommand.Dequeue().Do(resize);
        }
    }

    public void AddCommand<T>(T receivedCommand) where T : ICommand
    {
        switch (receivedCommand)
        {
            case Command_Deflate command:
                deflateCommand.Enqueue(command);
                break;
            case Command_Inflate command:
                inflateCommand.Enqueue(command);
                break;
            case Command_Move command:
                moveCommand.Enqueue(command);
                break;
            case Command_Rotate command:
                rotateCommand.Enqueue(command);
                break;
            case Command_Jump command:
                jumpCommand = command;
                break;
            case Command_Sidestep command:
                sidestepCommand = command;
                break;
        }
    }
}
