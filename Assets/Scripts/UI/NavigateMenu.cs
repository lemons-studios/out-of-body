using UnityEngine;
using UnityEngine.UI;
using LemonStudios.UI;
using UnityEngine.InputSystem;

public class NavigateMenu : MonoBehaviour
{
    public GameObject[] UIElements;
    public AudioClip menuNavigationSound;
    private PlayerInput uiActions;

    private int selectedElement = 0;
    
    private void OnEnable()
    {
        uiActions = new PlayerInput();
        uiActions.Menu.NavigateDown.performed += Navigate;
        uiActions.Menu.NavigateUp.performed += Navigate;
        uiActions.Enable();
    }
    
    private void OnDisable()
    {
        uiActions.Dispose();
    }

    private void Navigate(InputAction.CallbackContext ctx)
    {
        bool verticalNavigation = ctx.action.name.Contains("Up");
        UIElements[selectedElement].GetComponent<Image>().color = LemonUIUtils.CreateNormalizedColor(48, 52, 56);
        selectedElement = getNextIndex(selectedElement, verticalNavigation);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(menuNavigationSound);
        UIElements[selectedElement].GetComponent<Image>().color = LemonUIUtils.CreateNormalizedColor(34, 125, 230);
    }

    private int getNextIndex(int currentIndex, bool isNavigatingUp)
    {
        return isNavigatingUp switch
        {
            false => currentIndex - 1 < 0 ? UIElements.Length : currentIndex - 1,
            true => currentIndex + 1 > UIElements.Length ? 0 : currentIndex + 1
        };
    }
}
