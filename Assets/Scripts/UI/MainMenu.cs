using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip menuSelectionSound;
    public float menuSelectorMoveTime = 0.3f;
    
    private AudioSource mainMenuAudioSource;
    private Coroutine selectorCoroutine;
    private GameSettings gameSettings;
    private Image buttonSelector;
    private PlayerInput playerInput;
    private TextMeshProUGUI versionText;

    private void Start()
    {
        buttonSelector = GameObject.FindGameObjectWithTag("MenuSelector").GetComponent<Image>();
        versionText = GameObject.FindGameObjectWithTag("GameVersionText").GetComponent<TextMeshProUGUI>();
        mainMenuAudioSource = GetComponentInParent<AudioSource>();
        gameSettings = GetComponent<GameSettings>();
        
        // Initialize playerInput and set bindings
        playerInput = new PlayerInput();
        playerInput.UI.DisableSubmenu.performed += _ => toggleSubMenu(GameObject.FindGameObjectWithTag("SubMenu"));
        playerInput.Enable();
        
        // Set version on the version tmp asset
        versionText.text = $"Version {Application.version}";
    }
    /* Menu Button methods */
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void toggleSubMenu(GameObject menu)
    {
        if (menu == null) return;
        if (isSubmenuActive())
        {
            GameObject.FindGameObjectWithTag("SubMenu").SetActive(false);
        }
        menu.SetActive(!menu.activeSelf);
    }

    private bool isSubmenuActive()
    {
        return GameObject.FindGameObjectWithTag("SubMenu") != null;
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
    /* Menu Selector methods*/
    public void moveMenuSelectorHelper(float yPos)
    {
        // Prevent the selector bar from "moving" to a location it already is at
        if (yPos == buttonSelector.transform.localPosition.y) return;
        
        if (selectorCoroutine != null)
        {
            // Another layer of selector move prevention
            if (yPos == 135f || yPos == 0 || yPos == -135)
            {
                StopCoroutine(selectorCoroutine);
                selectorCoroutine = null;
            }
        }
        mainMenuAudioSource.PlayOneShot(menuSelectionSound);
        selectorCoroutine = StartCoroutine(moveMenuSelector(yPos, menuSelectorMoveTime));
    }

    private IEnumerator moveMenuSelector(float targetPosition, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float currentPosition = buttonSelector.transform.localPosition.y;
            float intermediateYPos = Mathf.Lerp(currentPosition, targetPosition, Mathf.SmoothStep(0, 1, elapsed / time));

            // value of ui on z axis should always be 0, since the ui is 2d
            buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x, intermediateYPos, 0);
            yield return null;
        }
        
        buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x, targetPosition, 0);
    }
    
    // Prevent playerInput from causing issues on subsequent reloads
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
