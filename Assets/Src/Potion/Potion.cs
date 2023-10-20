using UnityEngine;

public class Potion : MonoBehaviour
{
    public Color color;

    public Ingredient[] ingredients;
    // Start is called before the first frame update
    protected void Start()
    {
        var rend = GetComponent<Renderer>();
        rend.material.color = color;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
