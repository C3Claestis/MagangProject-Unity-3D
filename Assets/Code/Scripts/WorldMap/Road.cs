namespace WorldMap
{
    using System.Collections.Generic;
    using Dreamteck.Splines;
    using UnityEngine;


    public class Road : MonoBehaviour
    {
        [SerializeField] string roadName = "RoadName";
        [SerializeField] List<Destination> destinationList = new List<Destination>();
        private SplineComputer spline;
        private float distance;

        private void Awake()
        {
            spline = GetComponent<SplineComputer>();
        }

        private void Start()
        {
            gameObject.name = roadName;

            foreach (Destination destination in destinationList)
            {
                int index = destinationList.IndexOf(destination);
                int targetIndex;

                if (index == 0) targetIndex = 1;
                else            targetIndex = 0;

                destination.AddConnection(destinationList[targetIndex]);

                SetNearestPointPosition(destination.GetPosition());

            }
            
            distance = Vector3.Distance(destinationList[0].GetPosition(), destinationList[1].GetPosition());

        }

        private int FindNearestPoint(Vector3 targetPosition)
        {
            Vector3 firstPoint = spline.GetPoint(0).position;
            Vector3 LastPoint = spline.GetPoint(spline.GetPoints().Length - 1).position;
            float distancePath1 = Vector3.Distance(targetPosition, firstPoint);


            float distancePath2 = Vector3.Distance(targetPosition, LastPoint); if (distancePath1 < distancePath2) return 0;
            else return spline.GetPoints().Length - 1;
        }

        private void SetNearestPointPosition(Vector3 targetPosition)
        {
            int index = FindNearestPoint(targetPosition);
            spline.SetPointPosition(index, targetPosition);
        }

        public void ReversePointOrder(Vector3 targetPosition)
        {
            if (FindNearestPoint(targetPosition) == 0) return;

            SplinePoint[] points = spline.GetPoints();
            for (int i = 0; i < Mathf.FloorToInt(points.Length / 2); i++)
            {
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

            if (points.Length % 2 != 0)
            {
                Vector3 tempTan = points[Mathf.CeilToInt(points.Length / 2)].tangent;
                points[Mathf.CeilToInt(points.Length / 2)].tangent = points[Mathf.CeilToInt(points.Length / 2)].tangent2;
                points[Mathf.CeilToInt(points.Length / 2)].tangent2 = tempTan;
            }

            spline.SetPoints(points);
        }

        public bool CheckDestination(Destination target)
        {
            return destinationList.Contains(target);
        }

        public Destination GetDestinationByName(string targetName)
        {
            foreach (Destination destination in destinationList)
            {
                if (destination.GetName() != targetName) continue;
                return destination;
            }

            return null;
        }

        public float GetDistance() => distance;
        public string GetRoadName() => roadName;
        public List<Destination> GetDestinationList() => destinationList;
        public SplineComputer GetSpline() => spline;
    }
}