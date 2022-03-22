using UnityEngine;
using UnityEngine.InputSystem;
using LeRatTools;

public class ActionInputHandler : MonoBehaviour, ICommandClient
{
    // Command invoker reference
    private Player player;
    private Actions actions;

    private void Awake()
    {
        actions = new Actions();
        actions.TestMap1.Enable();
    }

    private void Update()
    {
        SelectPlayer();
        HandleInputMapChange();
        GenerateContinuousCommands();
        UpdateCamera();
    }

    void GenerateJumpCommand(InputAction.CallbackContext context)
    {
        player.AddCommand<Command_Jump>(new Command_Jump());
    }
    void GenerateInflateCommand(InputAction.CallbackContext context)
    {
        player.AddCommand<Command_Inflate>(new Command_Inflate());
    }
    void GenerateSidestepCommand(InputAction.CallbackContext context)
    {
        player.AddCommand<Command_Sidestep>(new Command_Sidestep());
    }
    void GenerateDeflateCommand(InputAction.CallbackContext context)
    {
        player.AddCommand<Command_Deflate>(new Command_Deflate());
    }

    private void SubscribeToActions()
    {
        actions.TestMap1.Jump.performed += GenerateJumpCommand;
        actions.TestMap2.Sidestep.performed += GenerateSidestepCommand;
    }
    private void UnsubscribeToActions()
    {
        actions.TestMap1.Jump.performed -= GenerateJumpCommand;
        actions.TestMap2.Sidestep.performed -= GenerateSidestepCommand;
    }
    private void ResubscribeToActions()
    {
        UnsubscribeToActions();
        SubscribeToActions();
    }

    private void SelectPlayer()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    player = hit.transform.GetComponent<Player>();
                    ResubscribeToActions();
                }
            }
        }
    }

    private void HandleInputMapChange()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (actions.TestMap1.enabled)
            {
                actions.TestMap1.Disable();
                actions.TestMap2.Enable();
            }
            else
            {
                actions.TestMap1.Enable();
                actions.TestMap2.Disable();
            }
        }
    }

    private void GenerateContinuousCommands()
    {
        if (player == null) return;

        Vector2 input;

        if (actions.TestMap1.Move.IsPressed())
        {
            input = actions.TestMap1.Move.ReadValue<Vector2>();
            player.AddCommand<Command_Move>(new Command_Move(new Vector3(input.x, 0f, input.y)));
        }

        if (actions.TestMap1.Inflate.IsPressed()) player.AddCommand<Command_Inflate>(new Command_Inflate());

        if (actions.TestMap2.Rotate.IsPressed())
        {
            input = actions.TestMap2.Rotate.ReadValue<Vector2>();
            player.AddCommand<Command_Rotate>(new Command_Rotate(new Vector3(input.y, 0f, -input.x)));
        }

        if (actions.TestMap2.Deflate.IsPressed()) player.AddCommand<Command_Deflate>(new Command_Deflate());
    }

    private void UpdateCamera()
    {
        if (player != null)
        {
            Camera.main.transform.position = player.transform.position;
            Camera.main.transform.position += Vector3.up * 5f + Vector3.back * 5f;
            Camera.main.transform.forward = player.transform.position - Camera.main.transform.position;
        }
    }
}