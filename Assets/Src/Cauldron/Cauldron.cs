using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    HashSet<Ingredient> elements;

    private RecipeCooker cooker;

    // Start is called before the first frame update
    void Start()
    {
        elements = new HashSet<Ingredient>();
        cooker = GetComponent<RecipeCooker>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Ingredient ingredient;
        if (collision.gameObject.TryGetComponent(out ingredient))
        {
            elements.Add(ingredient);
            Debug.Log("ADD Ingredient" + elements.Count);
            Cook();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Ingredient ingredient;
        if (collision.gameObject.TryGetComponent(out ingredient))
        {
            elements.Remove(ingredient);
            Debug.Log("REMOVE Ingredient" + elements.Count);
        }
    }

    void Cook()
    {
        if (elements.Count == 1)
            return;

        Potion potion;

        if (!cooker.Cook(elements, out potion))
            return;

        foreach (var element in elements)
        {
            Destroy(element.gameObject);
        }
        elements.Clear();

        var pos = transform.position;
        pos.y += 0.3f;

        Instantiate(potion, pos, transform.rotation);
    }
}
