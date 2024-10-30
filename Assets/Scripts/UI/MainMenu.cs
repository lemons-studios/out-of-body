using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Coroutine selectorCoroutine;
    private Image buttonSelector;

    private void Start()
    {
        buttonSelector = GameObject.FindGameObjectWithTag("MenuSelector").GetComponent<Image>();
    }

    public void moveMenuSelectorHelper(float yPos)
    {
        if (selectorCoroutine != null)
        {
            StopCoroutine(selectorCoroutine);
            selectorCoroutine = null;
        }

        selectorCoroutine = StartCoroutine(moveMenuSelector(yPos, 0.75f));
    }

    private IEnumerator moveMenuSelector(float targetPosition, float time = 0.4f)
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
