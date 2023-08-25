using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialPair
{
    public Material originalMaterial;
    public Material transparentMaterial;
}

public class ObjectFedOut : MonoBehaviour
{
    Renderer renderer;
    public MaterialPair[] materialPairs = new MaterialPair[2]; // Array ini harus memiliki 2 pasangan MaterialPair
    public bool isTransparent = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        UpdateMaterials();
    }

    private void Update()
    {
        if (isTransparent)
        {
            if (renderer.materials[0] != materialPairs[0].transparentMaterial ||
                renderer.materials[1] != materialPairs[1].transparentMaterial)
            {
                UpdateMaterials();
            }
        }
        else
        {
            if (renderer.materials[0] != materialPairs[0].originalMaterial ||
                renderer.materials[1] != materialPairs[1].originalMaterial)
            {
                UpdateMaterials();
            }
        }
    }

    private void UpdateMaterials()
    {
        Material[] newMaterials = new Material[] {
            isTransparent ? materialPairs[0].transparentMaterial : materialPairs[0].originalMaterial,
            isTransparent ? materialPairs[1].transparentMaterial : materialPairs[1].originalMaterial
        };

        renderer.materials = newMaterials;
    }

    public void ToggleTransparency()
    {
        isTransparent = !isTransparent;
    }
}


/*
   private void SetMaterialsTransparent(bool transparent)
   {
       Renderer renderer = GetComponent<Renderer>();
       Material[] materials = renderer.materials;

       // Loop melalui semua indeks material yang ingin diubah
       foreach (int index in transparentMaterialIndices)
       {
           if (index >= 0 && index < materials.Length)
           {
               if (transparent)
               {
                   // Setel material menjadi transparan
                   materials[index] = transparentMaterial[index];
               }
               else
               {
                   // Kembalikan material ke aslinya
                   materials[index] = originalMaterials[index];
               }
           }
       }

       // Update material pada objek
       renderer.materials = materials;
   }

   [SerializeField] private float _fadeSpeed, _fadeAmount;
   [SerializeField] Material asli1, asli2, transparan1, transparan2;
   float originalOpacity;
   Material [] material;
   [SerializeField] bool _Dofade = false;
   // Start is called before the first frame update
   void Start()
   {
       material = GetComponent<Renderer>().materials;
       foreach (Material mat in material)
       {
           originalOpacity = mat.color.a;
       }        
   }

   // Update is called once per frame
   void Update()
   {
       if (_Dofade)
       {
           FadeNow();
       }
       else { ResetFade(); }

   }
   void FadeNow()
   {
       foreach (Material mat in material)
       {
           Color currentcolor = mat.color;
           Color smoothColor = new Color(currentcolor.r, currentcolor.g, currentcolor.b,
               Mathf.Lerp(currentcolor.a, _fadeAmount, _fadeSpeed * Time.deltaTime));
           mat.color = smoothColor;
       }

   }
   void ResetFade()
   {
       foreach (Material mat in material)
       {
           Color currentcolor = mat.color;
           Color smoothColor = new Color(currentcolor.r, currentcolor.g, currentcolor.b,
               Mathf.Lerp(currentcolor.a, originalOpacity, _fadeSpeed * Time.deltaTime));
           mat.color = smoothColor;
       }
   }*/
