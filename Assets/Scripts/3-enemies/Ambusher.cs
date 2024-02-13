using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambusher : TargetMover
{
    [SerializeField] float ambushRange = 5f;

    [Tooltip("The object that we want to anbush")]
    [SerializeField] Transform targetObject = null;

    private SpriteRenderer _image;

    private void Awake()
    {
        _image = transform.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Hide()
    {
        _image.enabled = false;
    }

    private void Show()
    {
        _image.enabled = true;
    }

    private float DistanceToTarget() 
    {
        return Vector3.Distance(transform.position, targetObject.position);
    }

    private void Update() 
    {
        float distance = DistanceToTarget();
        // Check if the Ambusher is enabled, not at its target yet, and the other player is within range
        if (distance <= ambushRange)
        {
            // If hidden and player is within range, show the Ambusher
            Show();
            // Move towards the target position
            SetTarget(targetObject.position);
        }
        else
        {
            // If the Ambusher is not within range or already at its target, hide the Ambusher
            Hide();
        }    
    }
}
