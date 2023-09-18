namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Lazer
    {
        Vector3 pos, dir;

        GameObject laserObj;
        LineRenderer laser;
        List<Vector3> laserIndices = new List<Vector3>();

        /// <summary>
        /// Constructor Dari Lazer
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="material"></param>x
        public Lazer(Vector3 pos, Vector3 dir, Material material)
        {
            this.laser = new LineRenderer();
            this.laserObj = new GameObject();
            this.laserObj.name = "LaserBeam";
            this.pos = pos;
            this.dir = dir;

            this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
            this.laser.startWidth = 0.1f;
            this.laser.endWidth = 0.1f;
            this.laser.material = material;
            this.laser.startColor = Color.red;
            this.laser.endColor = Color.red;

            CastRay(pos, dir, laser);
        }

        void CastRay(Vector3 pos, Vector3 dir, LineRenderer lineRenderer)
        {
            laserIndices.Add(pos);

            Ray ray = new Ray(pos, dir);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit, 30, 1))
            {
                CheckHit(raycastHit, dir, lineRenderer);
            }
            else
            {
                laserIndices.Add(ray.GetPoint(30));
                UpdateLaser();
            }
        }

        void UpdateLaser()
        {
            int count = 0;
            laser.positionCount = laserIndices.Count;

            foreach(Vector3 idx in laserIndices)
            {
                laser.SetPosition(count, idx);
                count++;
            }
        }

        void CheckHit(RaycastHit hit, Vector3 dir, LineRenderer lineRenderer)
        {
            if (hit.collider.gameObject.tag == "PantulanLazer")
            {
                Vector3 pos = hit.point;
                Vector3 direction = Vector3.Reflect(dir, hit.normal);

                CastRay(pos, direction, lineRenderer);
            }
            else
            {
                laserIndices.Add(hit.point);
                UpdateLaser();
            }
        }
    }
}