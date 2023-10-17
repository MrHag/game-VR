using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    HashSet<Ingredient> elements;

    // Start is called before the first frame update
    void Start()
    {
        elements = new HashSet<Ingredient>();
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Ingredient potion;
            if(contact.otherCollider.gameObject.TryGetComponent(out potion))
            {
                elements.Add(potion);
                Debug.Log("Potion" + elements.Count);
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Ingredient potion;
            if(contact.otherCollider.gameObject.TryGetComponent(out potion))
            {
                elements.Remove(potion);
                Debug.Log("Potion" + elements.Count);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
