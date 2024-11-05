using System;
using System.Collections;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public GameObject[] movePositions;
    public float moveTime = 2.5f;
    public float waitTime = 1.5f;
    private int currentPosition;

    private void Start()
    {
        if (currentPosition > movePositions.Length) currentPosition = 0;
    }

    private IEnumerator moveToNextPosition()
    {
        while (gameObject.transform.position != movePositions[currentPosition].transform.position)
        {
            float elapsed = 0;
            Vector3 objectPosition = gameObject.transform.position;
            Vector3 targetPosition = movePositions[currentPosition].transform.position;
            while (elapsed < moveTime)
            {
                elapsed += Time.deltaTime;
                float x = Mathf.Lerp(objectPosition.x, targetPosition.x, elapsed / moveTime);
                float y = Mathf.Lerp(objectPosition.y, targetPosition.y, elapsed / moveTime);
                float z = Mathf.Lerp(objectPosition.z, targetPosition.z, elapsed / moveTime);
                
                Vector3 intermediatePosition = new Vector3(x, y, z);
                gameObject.transform.position = intermediatePosition;
                yield return null;
            }
        }

        yield return new WaitForSeconds(waitTime);
    }
    
    
    private int getNextPosition()
    {
        return currentPosition > movePositions.Length ? 0 : currentPosition++;
    }
}