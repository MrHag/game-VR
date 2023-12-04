using System;
using System.Collections;
using UnityEngine;

public class BottleTrigger : MonoBehaviour
{
    public event Action<Potion> PotionTriggered;

    public float triggerDelay;

    IEnumerator DelayTrigger(Potion potion)
    {
        yield return new WaitForSeconds(triggerDelay);
        PotionTriggered?.Invoke(potion);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Potion potion))
        {
            StartCoroutine(DelayTrigger(potion));
        }
    }
}
