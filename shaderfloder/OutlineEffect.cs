using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OutlineEffect : MonoBehaviour
{
    public Shader outlineShader;
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 0.01f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material outlineMaterial = new Material(outlineShader);
        outlineMaterial.SetColor("_Color", outlineColor);
        outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);

        renderer.materials = new Material[] { renderer.material, outlineMaterial };
    }
}
