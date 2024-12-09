using System.Collections;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private GameObject pusherObject;
    [SerializeField] private float extendWaitTime = 0.75f, retractWaitTime = 2.75f;
    [SerializeField] private float pusherMovementDistance = 2.5f;
    [SerializeField] private float pusherMoveTime = 0.25f;
    [SerializeField] private AudioClip extendSound, retractSound;
    private AudioSource sfxSource;
    private bool isExtended;
    public bool isExtending;

    private void Start()
    {
        StartCoroutine(PlatformMovementLoop());
        sfxSource = GameObject.FindGameObjectWithTag("GlobalSfx").GetComponent<AudioSource>();
    }

    private IEnumerator PlatformMovementLoop()
    {
        while (true)
        {
            float waitTime = isExtended ? retractWaitTime : extendWaitTime;
            float startPosY = pusherObject.transform.position.y;
            float targetPosY = startPosY + (isExtended ? -pusherMovementDistance : pusherMovementDistance);
            float elapsed = 0f;
            
            // sfxSource?.PlayOneShot(isExtended ? retractSound : extendSound);

            isExtending = true;
            while (elapsed < pusherMoveTime)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / pusherMoveTime;
                float smoothT = Mathf.SmoothStep(0f, 1f, t);

                Vector3 newPosition = pusherObject.transform.position;
                newPosition.y = Mathf.Lerp(startPosY, targetPosY, smoothT);
                pusherObject.transform.position = newPosition;

                yield return null; // Wait for the next frame
            }
            isExtending = false;

            // Floating point error check
            Vector3 finalPosition = pusherObject.transform.position;
            finalPosition.y = targetPosY;
            pusherObject.transform.position = finalPosition;

            isExtended = !isExtended;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
