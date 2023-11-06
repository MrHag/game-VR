using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TaggedElement<T>
{
    [HideInInspector]
    [SerializeField]
    private string name;

    public string tag;
    public T value;

    public string InspectorName { get { return name; } set { name = value; } }
}

public class TaggedContainer<T> : MonoBehaviour
{
    [SerializeField]
    public List<TaggedElement<T>> elements;


    protected void OnValidate()
    {
        bool hasDefault = false;

        if (elements != null)
            foreach (var elem in elements)
            {
                elem.InspectorName = elem.tag != "" ? elem.tag : "None";
                if (elem.tag == "Default")
                    hasDefault = true;
            }
        
        if(!hasDefault)
        {
            elements.Insert(0,new TaggedElement<T>(){tag = "Default", InspectorName = "Default"});
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
