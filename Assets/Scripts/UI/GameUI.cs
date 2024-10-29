using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI realDate, cameraNumber, levelTime;

    private void Start()
    {
        cameraNumber = GameObject.FindGameObjectWithTag("CameraText").GetComponent<TextMeshProUGUI>();
        realDate = GameObject.FindGameObjectWithTag("RealLifeTimeText").GetComponent<TextMeshProUGUI>();
        levelTime = GameObject.FindGameObjectWithTag("LevelTimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        levelTime.text = $"Time: {normalizeLevelTime(Mathf.FloorToInt(Time.timeSinceLevelLoad))}";
        realDate.text = $"{normalizeRealDate()} {normalizeRealTime(DateTime.Now.TimeOfDay.TotalSeconds)}";
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
}
