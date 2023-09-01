using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingBlockObject : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    [Range(0, 1f)]
    private float FadeAlpha = 0.33f;
    [SerializeField]
    private bool RetainShadows = true;
    [SerializeField]
    private Vector3 TargetPosisiOffset = Vector3.up;
    [SerializeField]
    private float FadeSpeed = 1f;

    [Header("Read Only!")]
    [SerializeField]
    private List<FadingObject> ObjectBlokingView = new List<FadingObject>();
    private Dictionary<FadingObject, Coroutine> RunningCorountine = new Dictionary<FadingObject, Coroutine>();
    private RaycastHit[] Hits = new RaycastHit[10];

    private void Start()
    {
        StartCoroutine(CheckForObjects());
    }
    private IEnumerator CheckForObjects()
    {
        while (true)
        {
            int hits = Physics.RaycastNonAlloc(
                Camera.transform.position, 
                (Target.transform.position + TargetPosisiOffset - Camera.transform.position).normalized,
                Hits,
                Vector3.Distance(Camera.transform.position, Target.transform.position + TargetPosisiOffset),
                layerMask
                );

            if(hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectFromHit(Hits[i]);

                    if(fadingObject != null && !ObjectBlokingView.Contains(fadingObject))
                    {
                        if (RunningCorountine.ContainsKey(fadingObject))
                        {
                            if(RunningCorountine[fadingObject] != null)
                            {
                                StopCoroutine(RunningCorountine[fadingObject]);
                            }
                            RunningCorountine.Remove(fadingObject);
                        }
                        RunningCorountine.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        ObjectBlokingView.Add(fadingObject);
                    }                    
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return null;
        }
    }

    private void FadeObjectsNoLongerBeingHit()
    {
        List<FadingObject> objectsToRemove = new List<FadingObject>(ObjectBlokingView.Count);

        foreach(FadingObject fadingObject in ObjectBlokingView)
        {
            bool objectIsBeingHit = false;
            for(int i = 0; i < Hits.Length; i++)
            {
                FadingObject hitFading = GetFadingObjectFromHit(Hits[i]);
                if(hitFading != null && fadingObject == hitFading)
                {
                    objectIsBeingHit = true;
                    break;
                }
            }

            if (!objectIsBeingHit)
            {
                if (RunningCorountine.ContainsKey(fadingObject))
                {
                    if(RunningCorountine[fadingObject] != null)
                    {
                        StopCoroutine(RunningCorountine[fadingObject]);
                    }

                    RunningCorountine.Remove(fadingObject);
                }

                RunningCorountine.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));
                objectsToRemove.Add(fadingObject);
            }
        }
        foreach(FadingObject removeObject in objectsToRemove)
        {
            ObjectBlokingView.Remove(removeObject);
        }
    }
    private IEnumerator FadeObjectOut(FadingObject fading)
    {
        foreach(Material material in fading.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.SetInt("_Surface", 1);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            material.SetShaderPassEnabled("DepthOnly", false);
            material.SetShaderPassEnabled("SHADOWCASTER", RetainShadows);

            material.SetOverrideTag("RenderType", "Transparent");

            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }
        float time = 0;

        while (fading.Materials[0].color.a > FadeAlpha)
        {
            foreach (Material material in fading.Materials)
            {
                if (material.HasProperty("_BaseColor"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(fading.initialAlpha, FadeAlpha, time * FadeSpeed)
                        );
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
        if (RunningCorountine.ContainsKey(fading))
        {
            StopCoroutine(RunningCorountine[fading]);
            RunningCorountine.Remove(fading);
        }
    }

    private IEnumerator FadeObjectIn(FadingObject fading)
    {
        float time = 0;

        while (fading.Materials[0].color.a < fading.initialAlpha)
        {
            foreach (Material material in fading.Materials)
            {
                if (material.HasProperty("_BaseColor"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(FadeAlpha, fading.initialAlpha, time * FadeSpeed)
                        );
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
        foreach (Material material in fading.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.SetInt("_Surface", 0);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            material.SetShaderPassEnabled("DepthOnly", true);
            material.SetShaderPassEnabled("SHADOWCASTER", true);

            material.SetOverrideTag("RenderType", "Opaque");

            material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
        
       if (RunningCorountine.ContainsKey(fading))
       {
           StopCoroutine(RunningCorountine[fading]);
           RunningCorountine.Remove(fading);
       }
    }
    private void ClearHits()
    {
        System.Array.Clear(Hits, 0, Hits.Length);
    }

    private FadingObject GetFadingObjectFromHit(RaycastHit hit)
    {
        return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
    }
}
