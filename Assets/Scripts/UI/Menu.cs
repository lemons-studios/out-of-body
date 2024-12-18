using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioClip menuSelectionSound;
    public GameObject pauseMenu;
    public Image buttonSelector;
    public TextMeshProUGUI versionText;
    public float menuSelectorMoveTime = 0.3f;
    
    private Coroutine selectorCoroutine;
    private PlayerInput playerInput;
    private AudioSource sceneAudioSource;

    private void Start()
    {
        // Initialize playerInput and set bindings
        playerInput = new PlayerInput();
        playerInput.UI.DisableSubmenu.performed += _ => EscapeActions();
        playerInput.Enable();
        sceneAudioSource = GameObject.FindGameObjectWithTag("GlobalSfx")?.GetComponent<AudioSource>();
        
        // Set version on the version tmp asset
        versionText.text = $"Version {Application.version}";
    }
    /* Menu Button methods */
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
    
    private void EscapeActions()
    {
        // Needed for pause menu resuming by escape key
        // If Menu is null, then the player is on the main pause menu with no submenus open, meaning that the escape key action should instead resume the game instead of trying to close a submenu

        if (isSubmenuActive())
        {
            GameObject.FindGameObjectWithTag("SubMenu").SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex != 0 && pauseMenu != null)
        {
            pauseMenu.SetActive(Time.timeScale != 0);
                
            // I love ternary expressions
            bool isPaused = Time.timeScale == 0;
            Cursor.lockState = isPaused ? CursorLockMode.Locked :  CursorLockMode.None; // why is this inversed
            Time.timeScale = isPaused ? 1 : 0;
        }
    }
    
    private bool isSubmenuActive()
    {
        return GameObject.FindGameObjectWithTag("SubMenu") != null;
    }

    public void ToggleSubmenu(GameObject SubMenu)
    {
        SubMenu.SetActive(!SubMenu.activeSelf);
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ResumeGame()
    {
        // Lazy fix to my game resume method
        EscapeActions();
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }
    
    /* Menu Selector methods*/
    public void moveMenuSelectorHelper(float yPos)
    {
        // Prevent the selector bar from "moving" to a location it already is at
        // ReSharper disable three CompareOfFloatsByEqualityOperator (positions are whole numbers and therefore won't have any funky floating point inaccuracies)
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
        sceneAudioSource.PlayOneShot(menuSelectionSound);
        selectorCoroutine = StartCoroutine(moveMenuSelector(yPos, menuSelectorMoveTime));
    }

    private IEnumerator moveMenuSelector(float targetPosition, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
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
