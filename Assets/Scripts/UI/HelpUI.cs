using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public GameObject helpUIContainer;
    public Image helpButton;
    public TextMeshProUGUI helpPromptText;
    
    private PlayerInput uiInput;
    
    private void Start()
    {
        uiInput = new PlayerInput();

        uiInput.UI.ShowControls.performed += _ => toggleHelpUI();
        uiInput.Enable();
    }

    private string getInputType()
    {
        var devices = InputSystem.devices;
        foreach (var device in devices)
        {
            if (device is Gamepad gamepad)
            {
                return gamepad.description.ToString().Contains("Xbox") ? "Xbox" : gamepad.description.ToString().Contains("DualShock") ? "PlayStation" : "Xbox" /*Just going to assume generic controllers mostly imitate the xbox layout*/;
            }
        }
        return "Keyboard";
    }

    private void toggleHelpUI()
    {
        bool toggle = !helpUIContainer.activeSelf;
        helpUIContainer.SetActive(toggle);
        helpPromptText.text = $"{boolToUI(toggle)} Controls";
    }

    private string boolToUI(bool status)
    {
        return status ? "Show" : "Hide";
    }
    
    private void setInputSprites()
    {
        string inputType = getInputType();
        Image[] bindImages = GetComponentsInChildren<Image>();
        helpButton.sprite = Resources.Load<Sprite>($"Textures/UI/Keybinds/{inputType}/Help");

        foreach (Image bindImage in bindImages)
        {
            // Lazy solution that most likely works
            TextMeshProUGUI tmpText = bindImage.GetComponentInChildren<TextMeshProUGUI>();
            bindImage.sprite = Resources.Load<Sprite>($"Textures/UI/Keybinds/{inputType}/{tmpText.text}");
        }
    }
}
