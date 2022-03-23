using UnityEngine;
using UnityEngine.InputSystem;
using LeRatTools;

public class RawInputHandler : MonoBehaviour, ICommandClient
{
    // Depends on the abstract invoker
    // Creates concrete commands
    // Pass concrete commands to abstract invokers

    public enum InputMap
    {
        FirstMap,
        SecondMap
    }
    [SerializeField] private InputMap map = InputMap.FirstMap;

    // Command invoker reference
    private Player player;

    private void Update()
    {
        SelectPlayer();
        HandleInputMapChange();
        HandleRecordingInputs();
        HandleCommandGeneration();
        UpdateCamera();
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
                }
            }
        }
    }

    private void HandleInputMapChange()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            map = (map == InputMap.FirstMap) ? map = InputMap.SecondMap : map = InputMap.FirstMap;
        }
    }

    private void HandleRecordingInputs()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (player != null)
            {
                player.RecordCommands();
            }
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (player != null)
            {
                player.PlayRecord();
            }
        }
    }

    private void HandleCommandGeneration()
    {
        if (player == null) return;

        Vector2 directional = Vector2.zero;
        directional.y += Keyboard.current.wKey.ReadValue();
        directional.y -= Keyboard.current.sKey.ReadValue();
        directional.x += Keyboard.current.dKey.ReadValue();
        directional.x -= Keyboard.current.aKey.ReadValue();
        bool specialKeyPressed = Keyboard.current.fKey.isPressed;
        bool spaceKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame;

        switch (map)
        {
            case InputMap.FirstMap:
                if (directional != Vector2.zero) player.AddCommand<Command_Move>(new Command_Move(new Vector3(directional.x, 0f, directional.y)));
                if (specialKeyPressed) player.AddCommand<Command_Inflate>(new Command_Inflate());
                if (spaceKeyPressed) player.AddCommand<Command_Jump>(new Command_Jump());
                break;
            case InputMap.SecondMap:
                if (directional != Vector2.zero) player.AddCommand<Command_Rotate>(new Command_Rotate(new Vector3(directional.y, 0f, -directional.x)));
                if (specialKeyPressed) player.AddCommand<Command_Deflate>(new Command_Deflate());
                if (spaceKeyPressed) player.AddCommand<Command_Sidestep>(new Command_Sidestep());
                break;
        }
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
