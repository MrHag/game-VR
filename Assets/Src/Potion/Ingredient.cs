using System;
using System.Collections;
using UnityEngine;

public class Ingredient : MonoBehaviour, IGrabbing, ISpawnControled
{

    private Coroutine unactivityTimer;

    //private Coroutine unactivityTimer;

    private float waitTime = 5.0f;

    public Spawner Spawner { get; set; }

    public event Action Spawn;

    private bool firstGrab = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnUnactivityTimer()
    {
        Destroy(this);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        OnUnactivityTimer();
    }

    public void OnGrabStart()
    {
        if (firstGrab)
        {
            firstGrab = false;
            Spawn?.Invoke();
        }
    }

    public void OnGrabEnd()
    {
        if (unactivityTimer != null)
        {
            StopCoroutine(unactivityTimer);
        }
        unactivityTimer = StartCoroutine(StartTimer());
    }
}
