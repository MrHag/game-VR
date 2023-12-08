using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkZone : MonoBehaviour
{

    int waitDestroySeconds = 5;

    static Dictionary<GameObject, (Coroutine, int)> objectsCoroutines = new Dictionary<GameObject, (Coroutine, int)>();

    IEnumerator DelayDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(waitDestroySeconds);
        if (!gameObject.IsDestroyed())
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IWorkZone component))
        {
            if (objectsCoroutines.TryGetValue(other.gameObject, out (Coroutine coroutine, int level) coroutineTuple))
            {
                coroutineTuple.level += 1;

                if (coroutineTuple.coroutine != null)
                    StopCoroutine(coroutineTuple.coroutine);

                objectsCoroutines[other.gameObject] = coroutineTuple;
            }
            else
            {

                component.ODestroy += OnObjectDestroy;

                objectsCoroutines.Add(other.gameObject, (null, 1));
            }

        }

    }

    void OnObjectDestroy(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IWorkZone component))
        {
            objectsCoroutines.Remove(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IWorkZone component))
        {
            if (objectsCoroutines.TryGetValue(other.gameObject, out (Coroutine coroutine, int level) coroutineTuple))
            {
                coroutineTuple.level -= 1;

                if (coroutineTuple.level <= 0)
                {
                    coroutineTuple.coroutine = StartCoroutine(DelayDestroy(other.gameObject));
                }

                objectsCoroutines[other.gameObject] = coroutineTuple;
            }
        }
    }
}
