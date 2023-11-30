using UnityEngine;

public class Potion : MonoBehaviour
{
    public Color color;

    public Color innerColor;

    public Ingredient[] ingredients;

    public GameObject liquidObject;

    public GameObject prefab;

    private ChangePivot pivotChanger;

    private Renderer rend;

    [Range(0.0f, 1.0f)]
    public float filling;

    public float max_filling;
    public float min_filling;

    private void OnValidate()
    {
        if (pivotChanger)
            CalcFilling();

        var matOverr = liquidObject.GetComponent<MaterialOverride>();

        var i = matOverr.overrideList.FindIndex(d => d.name == "_Color");
        var overrideData = matOverr.overrideList[i];
        overrideData.value = color;

        matOverr.overrideList[i] = overrideData;

        i = matOverr.overrideList.FindIndex(d => d.name == "_InnerColor");

        overrideData = matOverr.overrideList[i];
        overrideData.value = innerColor;

        matOverr.overrideList[i] = overrideData;

        matOverr.ApplyMaterialProperties();

        //block.SetColor("_Color", color);
        //block.SetColor("_InnerColor", innerColor);

    }

    private void CalcFilling()
    {
        pivotChanger.pivotPosition = new Vector3(0, -filling.Remap(0.0f, 1.0f, min_filling, max_filling), 0);
    }

    protected void Awake()
    {
        pivotChanger = liquidObject.GetComponent<ChangePivot>();
        rend = liquidObject.GetComponent<Renderer>();
    }

    protected void Start()
    {
        CalcFilling();

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        rend.GetPropertyBlock(block);

        block.SetColor("_Color", color);
        block.SetColor("_InnerColor", innerColor);
        rend.SetPropertyBlock(block);

        //var rend = GetComponent<Renderer>();
        //rend.material.color = color;
    }

}
