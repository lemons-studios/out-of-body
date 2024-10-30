using System;
using System.Collections;
using LemonStudios.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float menuSelectorMovetime = 0.3f;

    public AudioClip menuSelectionSound;

    private AudioSource mainMenuAudioSource;
    private Coroutine selectorCoroutine;
    private Image buttonSelector;
    private TextMeshProUGUI versionText;

    private void Start()
    {
        buttonSelector = GameObject.FindGameObjectWithTag("MenuSelector").GetComponent<Image>();
        versionText = GameObject.FindGameObjectWithTag("GameVersionText").GetComponent<TextMeshProUGUI>();
        mainMenuAudioSource = GetComponentInParent<AudioSource>();


        versionText.text = $"Version {Application.version}";
    }
    /* Menu Button methods */

    public void switchMenus()
    {
        LemonUIUtils.SwitchMenus();
    }
    
    public void openGithubLink()
    {
        Application.OpenURL("https://github.com/lemons-studios/out-of-body");
    }
    
    /* Menu Selector methods*/
    public void moveMenuSelectorHelper(float yPos)
    {
        if (selectorCoroutine != null)
        {
            StopCoroutine(selectorCoroutine);
            selectorCoroutine = null;
        }
        mainMenuAudioSource.PlayOneShot(menuSelectionSound);
        selectorCoroutine = StartCoroutine(moveMenuSelector(yPos, menuSelectorMovetime));
    }

    private IEnumerator moveMenuSelector(float targetPosition, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float currentPosition = buttonSelector.transform.localPosition.y;
            float intermediateYPos = Mathf.Lerp(currentPosition, targetPosition, Mathf.SmoothStep(0, 1, elapsed / time));

            // value of ui on z axis should always be 0 in my case
            buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x, intermediateYPos, 0);
            yield return null;
        }
        
        buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x, targetPosition, 0);
    }
}
