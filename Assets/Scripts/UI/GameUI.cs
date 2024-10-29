using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameUI : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(getInputType());
    }

    private string getInputType()
    {
        var devices = InputSystem.devices;
        foreach (var device in devices)
        {
            if (device is Gamepad gamepad)
            {
                return gamepad.description.ToString().Contains("Xbox") ? "Xbox" : gamepad.description.ToString().Contains("DualShock") ? "PlayStation" : "Generic" /*Just gonna assume generic controllers mostly imitate the xbox layout*/;
            }
        }
        return "Keyboard";
    }
}
