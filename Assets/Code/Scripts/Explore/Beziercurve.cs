using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beziercurve : MonoBehaviour
{
<<<<<<< HEAD:Assets/Code/Scripts/Beziercurve.cs
    public Transform[] controlPoints; // At least 3 points for a quadratic B�zier curve
=======
    public List<Transform> controlPoints = new List<Transform>(4); // At least 3 points for a quadratic B�zier curve
>>>>>>> UI-Menu:Assets/Code/Scripts/Explore/Beziercurve.cs

    private float CalculatePathLength()
    {
        //testting
        float pathLength = 0f;
        Vector3 previousPoint = GetPointOnBezier(0f);

        int segments = 100;
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = GetPointOnBezier(t);

            pathLength += Vector3.Distance(point, previousPoint);
            previousPoint = point;
        }

        return pathLength;
    }

    public float GetPathLength()
    {
        return CalculatePathLength();
    }

    public Vector3 GetPointOnBezier(float t)
    {
        int n = controlPoints.Count - 1;
        Vector3 point = Vector3.zero;

        for (int i = 0; i <= n; i++)
        {
            float blend = BinomialCoefficient(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
            point += blend * controlPoints[i].position;
        }

        return point;
    }

    private int BinomialCoefficient(int n, int k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    private int Factorial(int n)
    {
        if (n <= 1)
            return 1;
        return n * Factorial(n - 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (float t = 0; t <= 1; t += 0.01f)
        {
            Gizmos.DrawLine(GetPointOnBezier(t), GetPointOnBezier(t + 0.01f));
        }
    }    
}