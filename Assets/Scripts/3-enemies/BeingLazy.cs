using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingLazy : MonoBehaviour
{
    [Tooltip("How much time will the enemy will fall asleep?")]
    [SerializeField] float sleepTime = 6f;

    private bool isAsleep = false;

    private void Update() 
    {
        StartCoroutine(FallAsleep());
    }

    IEnumerator FallAsleep()
    {
        isAsleep = true;
        yield return new WaitForSeconds(sleepTime);
        isAsleep = false;
    }

    public bool GetStateSleep()
    {
        return isAsleep;
    }
}
