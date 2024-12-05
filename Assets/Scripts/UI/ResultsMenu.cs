using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsMenu : MonoBehaviour
{
    public void ContinueLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(0);
    }

    private void OnEnable() { Cursor.lockState = CursorLockMode.Confined; }
}
