using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeCooker : MonoBehaviour
{

    public Potion[] potions;

    // Start is called before the first frame update
    void Start()
    {

    }

    public bool Cook(ICollection<Ingredient> ingredients, out Potion pot)
    {
        pot = null;
        foreach (var potion in potions)
        {
            if (ingredients.Count != potion.ingredients.Length)
                continue;

            var list = new List<Ingredient>(potion.ingredients);

            var count = ingredients.Count((ing) =>
            {

                for (int i = 0; i < list.Count; i++)
                {
                    Ingredient otherIng = list[i];
                    if (ing.GetType() == otherIng.GetType())
                    {
                        list.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            });

            if (count == ingredients.Count)
            {
                pot = potion;
                return true;
            }
        }
        return false;
    }
}
