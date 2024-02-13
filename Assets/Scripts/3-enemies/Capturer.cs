using System.Collections;
using UnityEngine;

public class Capturer : TargetMover
{
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    [Tooltip("Range within which the enemy captures the player")]
    [SerializeField] float captureRange = 1f;

    [Tooltip("Time to hold the player after capture")]
    [SerializeField] float captureTime = 5f;

    [Tooltip("Time for the player to escape")]
    [SerializeField] float escapeTime = 3f;


    private bool isCapturing = false;
    private bool canCapture = true;

    [SerializeField] KeyboardMoverByTile playerMovementScript; // Corrected variable type

    protected override void Start() 
    {
        base.Start();
        playerMovementScript = targetObject.GetComponent<KeyboardMoverByTile>(); // Corrected component type
    }

    public Vector3 TargetObjectPosition() 
    {
        return targetObject.position;
    }

    private void Update() 
    {
        SetTarget(targetObject.position);

        if(canCapture)
        {
            if (Vector3.Distance(transform.position, targetObject.position) <= captureRange) {
                // Player is within capture range
                if (!isCapturing) 
                {
                    StartCoroutine(Capture());
                } 
            } 
            else 
            {
                // Player is outside capture range, stop capturing
                isCapturing = false;
            }
        }
    }

    IEnumerator Capture()
    {
        // Start capturing the player
        isCapturing = true;

        // Call method to disable movement in the player's movement script
        if (playerMovementScript != null)
        {
            playerMovementScript.DisableMovement();
            DisableMovement();
        }       

        yield return new WaitForSeconds(captureTime);

        // Call method to enable movement in the player's movement script
        if (playerMovementScript != null)
        {
            playerMovementScript.EnableMovement();
        }
        isCapturing = false;
        yield return new WaitForSeconds(escapeTime);
        EnableMovement();
    }

    private void EnableMovement()
    {
        canCapture = true;
    }

    private void DisableMovement()
    {
        canCapture = false;
    }
}
