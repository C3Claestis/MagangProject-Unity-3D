using System;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{
    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 pos;
    public List<Material> Materials = new List<Material>();
    [HideInInspector]
    public float initialAlpha;

    private void Awake()
    {
        pos = transform.position;

        if(Renderers.Count == 0)
        {
            Renderers.AddRange(GetComponentsInChildren<Renderer>());
        }
        foreach (Renderer renderer in Renderers)
        {
            Materials.AddRange(renderer.materials);
        }
        initialAlpha = Materials[0].color.a;
    }
    public bool Equals(FadingObject fading)
    {
        return pos.Equals(fading.pos);
    }
    public override int GetHashCode()
    {
        return pos.GetHashCode();
    }
}
