using System;
using Dreamteck.Splines;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] string roadName = "RoadName";
    private SplineComputer spline;

    private void Awake() {
        spline = GetComponent<SplineComputer>();
    }

    private void Start() {
        gameObject.name = roadName;
    }

    public void ReversePointOrder(){
        SplinePoint[] points = spline.GetPoints();
        for (int i = 0; i < Mathf.FloorToInt(points.Length / 2); i++){
            SplinePoint temp = points[i];
            points[i] = points[(points.Length - 1) - i];
            Vector3 tempTan = points[i].tangent;
            points[i].tangent = points[i].tangent2;
            points[i].tangent2 = tempTan;
            int opposideIndex = (points.Length - 1) - i;
            points[opposideIndex] = temp;
            tempTan = points[opposideIndex].tangent;
            points[opposideIndex].tangent = points[opposideIndex].tangent2;
            points[opposideIndex].tangent2 = tempTan;
        }

        if (points.Length % 2 != 0){
            Vector3 tempTan = points[Mathf.CeilToInt(points.Length / 2)].tangent;
            points[Mathf.CeilToInt(points.Length / 2)].tangent = points[Mathf.CeilToInt(points.Length / 2)].tangent2;
            points[Mathf.CeilToInt(points.Length / 2)].tangent2 = tempTan;
        }

        spline.SetPoints(points);
    }

    public int FindNearestPoint(Vector3 targetPosition){
        Vector3 firstPoint =  spline.GetPoint(0).position;
        Vector3 LastPoint =  spline.GetPoint(spline.GetPoints().Length-1).position;
        float distancePath1 = Vector3.Distance(targetPosition, firstPoint);
        float distancePath2 = Vector3.Distance(targetPosition, LastPoint);

        if (distancePath1 < distancePath2) return 0;
        else return spline.GetPoints().Length-1;
    }

    public void SetNearestPointPoisition(Vector3 targetPosition){
        int index = FindNearestPoint(targetPosition);
        spline.SetPointPosition(index, targetPosition);
    }


}
