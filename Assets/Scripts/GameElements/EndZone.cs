using TMPro;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameObject resultsUI;
    public TextMeshProUGUI timeStat, attemptsStat, rotationStat;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        GameObject player = other.gameObject;
        resultsUI.SetActive(true);
        rotationStat.text = $"Rotated {player.GetComponent<PlayerMovement>().GetAmountRotated()}\u00b0";
        
        string timeToComplete = GameObject.FindGameObjectWithTag("GameUIRoot").GetComponent<GameUI>().normalizeLevelTime(Mathf.FloorToInt(Time.timeSinceLevelLoad));
        timeStat.text = $"Time: {timeToComplete}";
        
        attemptsStat.text = $"Attempts: {GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>().GetAttempts()}";
    }
}
