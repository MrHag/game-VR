using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangePivot : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 startPivot;

    private Vector3 localStartPivot;

    [SerializeField]
    private Vector3 _pivotPosition;

    public Vector3 pivotPosition { get { return _pivotPosition; } set { _pivotPosition = value; MoveMesh(_pivotPosition); } }

    private Mesh mesh;
    private Vector3 pivot;

    void Start()
    { }
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        startPivot = transform.position;
        localStartPivot = transform.localPosition;
        pivot = startPivot;
        //var delta = startPivot - initialPivot;

        MoveMesh(pivotPosition);
    }

    private void OnValidate()
    {
        if (mesh)
        {
            MoveMesh(pivotPosition);
        }
    }

    public void MoveMesh(Vector3 point)
    {

        //var center = GetComponent<Renderer>().bounds.center;


        Vector3[] vert = mesh.vertices;

        for (var i = 0; i < vert.Length; i++)
        {
            vert[i] = startPivot + point + (vert[i] - pivot);
        }

        pivot = startPivot + point;

        transform.localPosition = localStartPivot - point;

        mesh.vertices = vert;

        mesh.RecalculateBounds();

    }

}
