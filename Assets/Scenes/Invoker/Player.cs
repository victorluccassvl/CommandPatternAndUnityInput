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

    private Recorder recorder;
    private bool playingRecord;
    private Rigidbody playerRigidbody;

    private Jump jump;
    private Move move;
    private Resize inflate;
    private Resize deflate;
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
        recorder = GetComponent<Recorder>();
        playingRecord = false;
        playerRigidbody = GetComponent<Rigidbody>();

        // Initialize command receivers
        jump = new Jump(playerRigidbody, jumpForce);
        move = new Move(playerRigidbody, moveSpeed);
        inflate = new Resize(transform);
        inflate.SetParameters(inflateRatio);
        deflate = new Resize(transform);
        deflate.SetParameters(deflateRatio);
        rotate = new Rotate(playerRigidbody, rotationForce);
        sidestep = new Sidestep(playerRigidbody, sidestepForce);
    }

    private void FixedUpdate()
    {
        if (playingRecord)
        {
            playingRecord = recorder.PlayRecords();
        }
        else
        {
            ExecuteCommands();
        }
    }

    public void RecordCommands()
    {
        if (recorder.IsRecording)
        {
            recorder.StopRecording();
        }
        else
        {
            recorder.StartRecording();
        }
    }

    public void PlayRecord()
    {
        playingRecord = true;
    }

    private void ExecuteCommands()
    {
        if (jumpCommand != null)
        {
            jumpCommand.Do(jump, recorder);
            jumpCommand = null;
        }

        if (sidestepCommand != null)
        {
            sidestepCommand.Do(sidestep, recorder);
            sidestepCommand = null;
        }

        while (rotateCommand.Count != 0)
        {
            rotateCommand.Dequeue().Do(rotate, recorder);
        }

        while (moveCommand.Count != 0)
        {
            moveCommand.Dequeue().Do(move, recorder);
        }

        while (inflateCommand.Count != 0)
        {
            inflateCommand.Dequeue().Do(inflate, recorder);
        }

        while (deflateCommand.Count != 0)
        {
            deflateCommand.Dequeue().Do(deflate, recorder);
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
