using UnityEngine;
using UnityEngine.InputSystem;
using LeRatTools;

public class Director_RawInput : Director<Actor_Player>
{
    public Actor_Player actorDirectReference;
    public bool useQueue = true;

    public enum InputMap
    {
        FirstMap,
        SecondMap
    }

    [SerializeField] private InputMap map = InputMap.FirstMap;

    private void Awake()
    {
        this.SetActor(actorDirectReference);
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            map = (map == InputMap.FirstMap)? InputMap.SecondMap : InputMap.FirstMap;
        }

        if (actorDirectReference != currentActor)
        {
            this.SetActor(actorDirectReference);
        }

        switch (map)
        {
            case InputMap.FirstMap:
                GenerateFirstMapInputs();
                return;
            case InputMap.SecondMap:
                GenerateSecondMapInputs();
                return;
        }
    }

    private void GenerateFirstMapInputs()
    {
        Command_Move move = null;
        Command_Deflate deflate = null;
        Command_Jump jump = null;

        Vector2 movement = Vector2.zero;
        movement.y += Keyboard.current.wKey.ReadValue();
        movement.y -= Keyboard.current.sKey.ReadValue();
        movement.x += Keyboard.current.dKey.ReadValue();
        movement.x -= Keyboard.current.aKey.ReadValue();
        move = new Command_Move(movement, 0.1f);

        if (Mouse.current.leftButton.IsPressed())
            deflate = new Command_Deflate(0.999f);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            jump = new Command_Jump(ForceMode.Impulse, 10f);

        if (useQueue)
        {
            if (move != null) currentActor.ExecuteComand(move);
            if (deflate != null) currentActor.ExecuteComand(deflate);
            if (jump != null) currentActor.ExecuteComand(jump);
        }
        else
        {
            if (move != null) EnqueueNextFixedUpdateCommand(move);
            if (deflate != null) EnqueueNextFixedUpdateCommand(deflate);
            if (jump != null) EnqueueNextFixedUpdateCommand(jump);
        }
    }

    private void GenerateSecondMapInputs()
    {
        Command_Rotate rotate = null;
        Command_Inflate inflate = null;
        Command_Teleport teleport = null;

        Vector2 rotation = Vector2.zero;
        rotation.y += Keyboard.current.wKey.ReadValue();
        rotation.y -= Keyboard.current.sKey.ReadValue();
        rotation.x += Keyboard.current.dKey.ReadValue();
        rotation.x -= Keyboard.current.aKey.ReadValue();
        rotate = new Command_Rotate(rotation, 0.1f);

        if (Mouse.current.leftButton.IsPressed())
            inflate = new Command_Inflate(1.001f);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            teleport = new Command_Teleport(4f);

        if (useQueue)
        {
            if (rotate != null) currentActor.ExecuteComand(rotate);
            if (inflate != null) currentActor.ExecuteComand(inflate);
            if (teleport != null) currentActor.ExecuteComand(teleport);
        }
        else
        {
            if (rotate != null) EnqueueNextFixedUpdateCommand(rotate);
            if (inflate != null) EnqueueNextFixedUpdateCommand(inflate);
            if (teleport != null) EnqueueNextFixedUpdateCommand(teleport);
        }
    }
}
