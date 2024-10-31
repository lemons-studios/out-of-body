using UnityEditor;
using UnityEngine;

public class DeletePlayerPrefsKeys : MonoBehaviour
{
    [MenuItem("Tools/Delete PlayerPrefs keys")]
    public static void RemovePlayerPrefsKeys()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs keys have been deleted.");
    }
}
