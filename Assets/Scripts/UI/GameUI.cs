using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI realDate, cameraText, levelTime;
    
    private void Start()
    {
        cameraText = GameObject.FindGameObjectWithTag("CameraText").GetComponent<TextMeshProUGUI>();
        realDate = GameObject.FindGameObjectWithTag("RealLifeTimeText").GetComponent<TextMeshProUGUI>();
        levelTime = GameObject.FindGameObjectWithTag("LevelTimeText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateGameUI());
    }

    private string normalizeLevelTime(int rawTime)
    {
        return TimeSpan.FromSeconds(rawTime).ToString(@"mm\:ss");
    }

    private string normalizeRealTime(double timeSinceMidnight)
    {
        TimeSpan currentTime = TimeSpan.FromSeconds(timeSinceMidnight);
        return currentTime.ToString(@"hh\:mm\:ss");
    }
    
    private string normalizeRealDate()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime.ToString("dd-MM-yyyy");
    }

    public void setCameraText(int cameraNumber)
    {
        cameraText.text = $"Camera #{cameraNumber}";
    }

    private IEnumerator UpdateGameUI()
    {
        // Much more efficient than putting this in Update()
        while (true)
        {
            levelTime.text = $"Time: {normalizeLevelTime(Mathf.FloorToInt(Time.timeSinceLevelLoad))}";
            realDate.text = $"{normalizeRealDate()} {normalizeRealTime(DateTime.Now.TimeOfDay.TotalSeconds)}";
            yield return new WaitForSeconds(1);
        }
    }
}
