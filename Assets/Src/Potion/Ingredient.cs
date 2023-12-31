using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ingredient : MonoBehaviour, IGrabbing, ISpawnControled, IWorkZone
{

    private Coroutine unactivityTimer;

    //private Coroutine unactivityTimer;

    private float waitTime = 5.0f;

    public Spawner Spawner { get; set; }

    public event Action Spawn;

    private bool firstGrab = true;

    public event Action<GameObject> ODestroy;

    [SerializeField]
    private string _ingredientName;
    public string IngredientName { get => _ingredientName; }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnDestroy()
    {
        ODestroy?.Invoke(gameObject);
    }

    void OnValidate()
    {
        _ingredientName = name;
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
        // if (firstGrab)
        // {
        //     firstGrab = false;
        //     Spawn?.Invoke();
        // }
    }

    public void OnGrabEnd()
    {
        // if (unactivityTimer != null)
        // {
        //     StopCoroutine(unactivityTimer);
        // }
        // unactivityTimer = StartCoroutine(StartTimer());
    }
}
