using TMPro;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameObject resultsUI;
    public TextMeshProUGUI timeStat, attemptsStat, rotationStat;
    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;
        if (!collidedObject.CompareTag("Player")) return;
        collidedObject.GetComponent<Rigidbody>().isKinematic = true;
        
        resultsUI.SetActive(true);
        rotationStat.text = $"Rotated {collidedObject.GetComponent<PlayerMovement>().GetAmountRotated()}\u00b0"; // u00b0 being the degree symbol
        
        string timeToComplete = GameObject.FindGameObjectWithTag("GameUIRoot").GetComponent<GameUI>().normalizeLevelTime(Mathf.FloorToInt(Time.timeSinceLevelLoad));
        timeStat.text = $"Time: {timeToComplete}";
        
        attemptsStat.text = $"Attempts: {GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>().GetAttempts()}";
    }
}
