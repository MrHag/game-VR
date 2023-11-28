using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public enum Side
{
    Left,
    Right
}

[Serializable]
public class Book : MonoBehaviour
{
    public TaggedTextureContainer editor_textures;

    private Dictionary<string, Texture2D> textures;

    public MeshRenderer leftMeshRender;

    public MeshRenderer rightMeshRender;

    private Coroutine _leftTransition;

    private Coroutine _rightTransition;

    IEnumerator Transition(float time, int steps, MeshRenderer mesh, string tag, Action callback)
    {
        var stepTime = time / steps;
        var opacity = mesh.material.GetFloat("_Opacity");
        var opacityStep = (1.0f - opacity) / steps;

        for (int step = 0; step < steps; step++)
        {
            opacity += opacityStep;
            yield return new WaitForSeconds(stepTime);
            mesh.material.SetFloat("_Opacity", opacity);
        }

        mesh.material.SetTexture("_Texture", textures[tag]);

        opacity = 1.0f;
        opacityStep = 1.0f / steps;

        for (int step = 0; step < steps; step++)
        {
            opacity -= opacityStep;
            yield return new WaitForSeconds(stepTime);
            mesh.material.SetFloat("_Opacity", opacity);
        }

        callback();
    }



    public void LoadTexture(string tag, Side side)
    {

        if (side == Side.Left)
        {
            MeshRenderer mesh = leftMeshRender;

            Action callback = () => _leftTransition = null;

            if (_leftTransition != null)
                StopCoroutine(_leftTransition);

            _leftTransition = StartCoroutine(Transition(2.0f, 100, mesh, tag, callback));

        }
        else
        {
            MeshRenderer mesh = rightMeshRender;

            Action callback = () => _rightTransition = null;

            if (_rightTransition != null)
                StopCoroutine(_rightTransition);

            _rightTransition = StartCoroutine(Transition(2.0f, 100, mesh, tag, callback));

        }

        // MeshRenderer mesh = side == Side.Left ? leftMeshRender : rightMeshRender;
    }

    // Start is called before the first frame update
    void Start()
    {
        textures = new Dictionary<string, Texture2D>();
        foreach (var elem in editor_textures.elements)
        {
            textures[elem.tag] = elem.value;
        }

        LoadTexture("Rec1", Side.Left);
        LoadTexture("Rec2", Side.Right);
    }
}