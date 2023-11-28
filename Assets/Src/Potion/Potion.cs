using UnityEngine;

public class Potion : MonoBehaviour
{
    public Color color;

    public Ingredient[] ingredients;

    public GameObject liquidObject;

    private ChangePivot pivotChanger;

    [Range(0.0f, 1.0f)]
    public float filling;

    public float max_filling;
    public float min_filling;

    private void OnValidate()
    {
        if (pivotChanger)
            CalcFilling();
    }

    private void CalcFilling()
    {
        pivotChanger.pivotPosition = new Vector3(0, -filling.Remap(0.0f, 1.0f, min_filling, max_filling), 0);
    }

    protected void Awake()
    {
        pivotChanger = liquidObject.GetComponent<ChangePivot>();

    }

    protected void Start()
    {
        CalcFilling();
        //var rend = GetComponent<Renderer>();
        //rend.material.color = color;
    }
}
