using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Path : MonoBehaviour
{

    private Beziercurve SplineCurve { get; set; }
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public IntersectionPoint startIntersectionPoint;
    public IntersectionPoint endIntersectionPoint;

    private void OnValidate()
    {
        GameObject parent = gameObject;
        Vector3 parentPosition = parent.transform.position;

        if (SplineCurve)
        {
            startPoint      ??= SplineCurve.controlPoints[0];
            endPoint        ??= SplineCurve.controlPoints[^1];
        }

        if (startIntersectionPoint && !startIntersectionPoint.paths.Find(obj => obj == this))
        {
            startIntersectionPoint.paths.Add(this);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void UpdatePosition()
    {
        if (startIntersectionPoint)
        {
            startPoint.transform.position = startIntersectionPoint.transform.position;
        }
        
        if (endIntersectionPoint)
        {
            endPoint.transform.position = endIntersectionPoint.transform.position;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdatePosition();
        }

    
    }
}
