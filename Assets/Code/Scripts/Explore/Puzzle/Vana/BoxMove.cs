namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BoxMove : MonoBehaviour
    {
        [SerializeField] float magnitudeMove;
        [SerializeField] float raycastDistance = 1.0f; // Jarak raycast
        [SerializeField] LayerMask raycastLayerMask; // Layer mask untuk raycast
        public bool isDestroy = false;

        private void Update()
        {
            ScanRaycast();

            if (isDestroy)
            {
                ObjectDiDestroy();
                isDestroy = false;
            }
        }
        void ObjectDiDestroy()
        {
            ObstacleBroke[] obstacle = FindObjectsOfType<ObstacleBroke>();
            foreach (ObstacleBroke obs in obstacle)
            {
                if (obs.GetIsBroke())
                {
                    Destroy(obs.gameObject.transform.parent.gameObject, 4f);
                }
            }
        }
        /// <summary>
        /// Untuk Mengecek Apakah Ada Box Didepannya
        /// </summary>
        void ScanRaycast()
        {
            // Membuat raycast ke depan dari posisi objek
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Lakukan raycast
            if (Physics.Raycast(ray, out hit, raycastDistance, raycastLayerMask))
            {
                // Jika raycast mengenai objek "Boks", gerakkan objek tersebut
                if (hit.collider.gameObject.name == "Boks")
                {
                    Rigidbody rigidbody = hit.collider.attachedRigidbody;

                    if (rigidbody != null)
                    {
                        Vector3 forceDir = hit.point - transform.position;
                        forceDir.y = 0;
                        forceDir.Normalize();

                        rigidbody.AddForceAtPosition(forceDir * magnitudeMove, hit.point, ForceMode.Impulse);
                    }
                }
                // Menggambar raycast menggunakan Debug.DrawRay
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
            }
        }
    }
}