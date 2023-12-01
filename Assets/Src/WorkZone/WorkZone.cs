using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkZone : MonoBehaviour
{

    int waitDestroySeconds = 5;

    Dictionary<GameObject, Coroutine> objectsCoroutines;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator DelayDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(waitDestroySeconds);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out IWorkZone component))
        {

            if (objectsCoroutines.TryGetValue(other.gameObject, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);

                objectsCoroutines.Remove(other.gameObject);
            }
        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out IWorkZone component))
        {
            var coroutine = StartCoroutine(DelayDestroy(other.gameObject));

            objectsCoroutines.Add(other.gameObject, coroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
