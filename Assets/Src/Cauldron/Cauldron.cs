using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    List<string> elements;

    public Smoke smoke;

    private RecipeCooker cooker;

    // Start is called before the first frame update
    void Start()
    {
        elements = new List<string>();
        cooker = GetComponent<RecipeCooker>();
    }

    void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient;
        if (other.TryGetComponent(out ingredient))
        {
            elements.Add(ingredient.IngredientName);
            Debug.Log("ADD Ingredient" + ingredient.IngredientName);
            Destroy(ingredient.gameObject);
            Cook();
        }
    }

    // void OnCollisionExit(Collision collision)
    // {
    //     Ingredient ingredient;
    //     if (collision.gameObject.TryGetComponent(out ingredient))
    //     {
    //         elements.Remove(ingredient);
    //         Debug.Log("REMOVE Ingredient" + elements.Count);
    //     }
    // }

    IEnumerator SpawnPotionDelay(float delay, Potion potion)
    {
        //var pos = transform.position;
        var pos = new Vector3(0.572000027f, 1.30900002f, -3.28299999f);
        pos.y += 0.3f;

        yield return new WaitForSeconds(delay);

        var gameObject = Instantiate(potion.prefab, pos, transform.rotation);

        if (gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }
    }

    void Cook()
    {
        // if (elements.Count == 1)
        //     return;

        Potion potion;

        bool possiblyCook;

        if (!cooker.Cook(elements, out potion, out possiblyCook))
        {
            if (!possiblyCook)
            {
                print("CANT COOK");
                smoke.Play(1.0f, Color.red);
                elements.Clear();
                return;
            }
            print("CAN COOK");
            smoke.Play(1.0f, Color.yellow);
            return;
        }

        print("COOKING");

        smoke.Play(3.0f, Color.green);

        elements.Clear();

        StartCoroutine(SpawnPotionDelay(3.0f, potion));

    }
}
